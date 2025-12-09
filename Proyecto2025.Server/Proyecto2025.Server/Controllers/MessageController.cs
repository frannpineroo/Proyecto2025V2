using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Shared.DTO;
using Proyecto2025.Shared.ENUM;
using Microsoft.AspNetCore.SignalR;
using Proyecto2025.Server.Hubs;



namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/message")]
    
    public class MessageController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly IMensajeRepositorio mensajeRepository;
        //private readonly IMapper mapper;
        public MessageController(AppDbContext context, IHubContext<MessageHub> hubContext, IMensajeRepositorio mensajeRepositorio)
        {
            this.context = context;
            _hubContext = hubContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetAll()
        {
            var messages = await context.Messages
                .Include(m => m.Chat)
                .Include(m => m.Sender)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
            return Ok(messages);
        }

        [HttpGet("chat/{chatId:long}")]
        public async Task<ActionResult<List<Message>>> GetByChat(long chatId)
        {
            var messages = await context.Messages
                .Include(m => m.Sender)
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            return Ok(messages);
        }



        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Message>> GetByid(int Id)
        {
            var messaje = await context.Messages.FirstOrDefaultAsync(x => x.Id == Id);
            if (messaje is null)
            {
                return NotFound($"No existe el Mensaje con id {Id}");
            }
            return Ok(messaje);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearMensajeDTO dto)
        {
            if (dto == null)
                return BadRequest("El mensaje recibido está vacío.");

            if (string.IsNullOrWhiteSpace(dto.Content))
                return BadRequest("El mensaje no puede estar vacío.");

            // Verificar que el Chat exista
            var chatExiste = await context.Chats.AnyAsync(c => c.Id == dto.ChatId);
            if (!chatExiste)
                return BadRequest($"No existe un chat con Id = {dto.ChatId}");

            // Convertir el string del DTO al enum
            if (!Enum.TryParse<MessageType>(dto.MessageType, true, out var tipoMensaje))
            {
                tipoMensaje = MessageType.text; // valor por defecto
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

            //construccion de de payload que el cliente entienda 
            var payload = new
            {
                ChatId = dto.ChatId,
                SenderId = dto.SenderId,
                Content = dto.Content,
                MediaFile = dto.MediaFile,
                MessageType = tipoMensaje,
                SentAt = dto.SentAt
            };

            //enviar el mensaje a un grupo
            await _hubContext.Clients.Group($"chat-{dto.ChatId}").SendAsync("ReceiveMessage", payload);
            return Ok(nuevo.Id);
        }







        [HttpPut("ocultar")]
        public async Task<ActionResult<int>> Put(Proyecto2025.Shared.DTO.OcultarMensajeDTO DTO)
        {
            var mensaje = await context.Messages.FirstOrDefaultAsync(x => x.Id == DTO.MensajeId);
            if (mensaje == null)
            {
                return NotFound($"No existe el Mensaje con id {DTO.MensajeId}");
            }
             mensaje.IsArchived = true;
             context.Messages.Update(mensaje);
             await context.SaveChangesAsync();
             return Ok(($"Mensaje ocultado correctamente"));
        }


    }
}
