using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Shared.DTO;
using Proyecto2025.Shared.ENUM;
using Microsoft.AspNetCore.SignalR;
using Proyecto2025.Server.Hubs;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class MessageController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly IMensajeRepositorio mensajeRepository;

        public MessageController(AppDbContext context, IHubContext<MessageHub> hubContext, IMensajeRepositorio mensajeRepository)
        {
            this.context = context;
            _hubContext = hubContext;
            this.mensajeRepository = mensajeRepository;
        }

        // Devolver DTOs, no entidades EF directamente
        [HttpGet]
        public async Task<ActionResult<List<VerMensajesDTO>>> GetAll()
        {
            // Obtener Id del usuario actual desde las claims. Si no está autenticado se considera 0.
            long currentRoleId = 0;
            if (User?.Identity?.IsAuthenticated == true)
            {
                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("id")?.Value;
                long.TryParse(idClaim, out currentRoleId);
            }

            var messages = await context.Messages
                .Include(m => m.Chat)
                .Include(m => m.Sender)
                // Si el mensaje está archivado, solo mostrarlo si el usuario tiene RoleId == 1
                .Where(m => !m.IsArchived || currentRoleId == 1)
                .OrderBy(m => m.SentAt)
                .Select(m => new VerMensajesDTO
                {
                    Id = (int)m.Id,
                    ChatId = (int)m.ChatId,
                    ChatName = m.Chat != null ? m.Chat.Name ?? string.Empty : string.Empty,
                    SenderId = (int)m.SenderId,
                    SenderName = m.Sender != null ? m.Sender.FirstName + " " + m.Sender.LastName : string.Empty,
                    Content = m.Content,
                    MessageType = (int)m.MessageType,
                    MediaFile = m.MediaFile != null ? Convert.ToBase64String(m.MediaFile) : null,
                    SentAt = m.SentAt,
                    IsRead = m.IsRead,
                    IsArchived = m.IsArchived
                })
                .ToListAsync();

            return Ok(messages);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<VerMensajesDTO>> GetByid(int Id)
        {
            long currentUserId = 0;
            if (User?.Identity?.IsAuthenticated == true)
            {
                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("id")?.Value;
                long.TryParse(idClaim, out currentUserId);
            }

            var m = await context.Messages
                .Include(x => x.Chat)
                .Include(x => x.Sender)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (m is null) return NotFound($"No existe el Mensaje con id {Id}");

            // Si el mensaje está archivado y el usuario no es el admin (Id 1), no exponerlo
            if (m.IsArchived && currentUserId != 1) return NotFound($"No existe el Mensaje con id {Id}");

            var dto = new VerMensajesDTO
            {
                Id = (int)m.Id,
                ChatId = (int)m.ChatId,
                ChatName = m.Chat?.Name ?? string.Empty,
                SenderId = (int)m.SenderId,
                SenderName = m.Sender != null ? $"{m.Sender.FirstName} {m.Sender.LastName}" : string.Empty,
                Content = m.Content,
                MessageType = (int)m.MessageType,
                MediaFile = m.MediaFile != null ? Convert.ToBase64String(m.MediaFile) : null,
                SentAt = m.SentAt,
                IsRead = m.IsRead,
                IsArchived = m.IsArchived
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearMensajeDTO dto)
        {
            if (dto == null) return BadRequest("El mensaje recibido está vacío.");
            if (string.IsNullOrWhiteSpace(dto.Content)) return BadRequest("El mensaje no puede estar vacío.");

            var chatExiste = await context.Chats.AnyAsync(c => c.Id == dto.ChatId);
            if (!chatExiste) return BadRequest($"No existe un chat con Id = {dto.ChatId}");

            if (!Enum.TryParse<MessageType>(dto.MessageType, true, out var tipoMensaje))
            {
                tipoMensaje = MessageType.text;
            }

            var nuevo = new Message
            {
                ChatId = dto.ChatId,
                SenderId = dto.SenderId,
                Content = dto.Content,
                MediaFile = dto.MediaFile,
                MessageType = tipoMensaje,
                SentAt = dto.SentAt
            };

            context.Messages.Add(nuevo);
            await context.SaveChangesAsync();

            // recuperar con relaciones para construir el DTO con SenderName
            var saved = await context.Messages
                .Include(m => m.Chat)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == nuevo.Id);

            var verDto = new VerMensajesDTO
            {
                Id = (int)saved!.Id,
                ChatId = (int)saved.ChatId,
                ChatName = saved.Chat?.Name ?? string.Empty,
                SenderId = (int)saved.SenderId,
                SenderName = saved.Sender != null ? $"{saved.Sender.FirstName} {saved.Sender.LastName}" : string.Empty,
                Content = saved.Content,
                MessageType = (int)saved.MessageType,
                MediaFile = saved.MediaFile != null ? Convert.ToBase64String(saved.MediaFile) : null,
                SentAt = saved.SentAt,
                IsRead = saved.IsRead,
                IsArchived = saved.IsArchived
            };

            await _hubContext.Clients.Group($"chat-{dto.ChatId}").SendAsync("ReceiveMessage", verDto);
            return Ok(nuevo.Id);
        }

        [HttpPut("ocultar")]
        public async Task<ActionResult<string>> Put(Proyecto2025.Shared.DTO.OcultarMensajeDTO DTO)
        {
            var mensaje = await context.Messages.FirstOrDefaultAsync(x => x.Id == DTO.MensajeId);
            if (mensaje == null) return Ok("Mensaje inexistente o oculto");

            mensaje.IsArchived = true;
            context.Messages.Update(mensaje);
            await context.SaveChangesAsync();
            return Ok(new { mensaje = "Mensaje ocultado" });
        }

        [HttpGet("chat/{chatId:int}")]
        public async Task<ActionResult<List<VerMensajesDTO>>> GetMessagesByChatId(int chatId)
        {
            // Obtener Id del usuario actual desde las claims
            long currentRoleId = 0;
            if (User?.Identity?.IsAuthenticated == true)
            {
                var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("id")?.Value;
                long.TryParse(idClaim, out currentRoleId);
            }

            var messages = await context.Messages
                .Where(m => m.ChatId == chatId &&
                            (!m.IsArchived || currentRoleId == 1)) // admin ve archivados
                .Include(m => m.Sender)
                .Include(m => m.Chat)
                .OrderBy(m => m.SentAt)
                .Select(m => new VerMensajesDTO
                {
                    Id = (int)m.Id,
                    ChatId = (int)m.ChatId,
                    ChatName = m.Chat != null ? m.Chat.Name ?? "" : "",
                    SenderId = (int)m.SenderId,
                    SenderName = m.Sender != null ? m.Sender.FirstName + " " + m.Sender.LastName : "",
                    Content = m.Content,
                    MessageType = (int)m.MessageType,
                    MediaFile = m.MediaFile != null ? Convert.ToBase64String(m.MediaFile) : null,
                    SentAt = m.SentAt,
                    IsRead = m.IsRead,
                    IsArchived = m.IsArchived
                })
                .ToListAsync();

            return Ok(messages);
        }

    }
}
