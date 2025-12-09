using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/ChatMember")]
    public class ChatMemberController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IChatMemberRepositorio<ChatMember> chatMemberRepositorio;

        public ChatMemberController(AppDbContext context,
            IChatMemberRepositorio<ChatMember> chatMemberRepositorio)
        {
            this.context = context;
            this.chatMemberRepositorio = chatMemberRepositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<ChatMember>>> GetAllChatMembers()
        {
            var members = await chatMemberRepositorio.GetEsAsync();

            if (members == null)
            {
                return NotFound("No se encontraron chats, verifique de nuevo.");
            }

            //{
            //    return Ok(new List<ListaChatMembersDTO>()); // Retorna una lista vacía si no hay miembros con un 200 OK
            //}
            //mapeo de entidades a DTOs
            //var listaDTO = members.Select(member => new ListaChatMembersDTO
            //{
            //    Id = member.Id,
            //    ChatId = member.ChatId,
            //    UserId = member.UserId,
            //    IsModerator = member.IsModerator,
            //    CanWrite = member.CanWrite,
            //    JoinedAt = member.JoinedAt
            //}).ToList();

            return Ok(members);

        }
        #region
        [HttpGet("por-chat/{chatId}")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> GetChatMembers(long chatId)
        {
            {
                var members = await chatMemberRepositorio.SelectByChatId(chatId);

                if (members == null || members.Count == 0)
                {
                    return NotFound("No members found for this chat.");
                }
                return Ok(members);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ChatMember>> InsertMiembro([FromBody] ChatMemberDTO dto)
        {
            var yaExiste = await context.ChatMembers
                .AnyAsync(cm => cm.ChatId == dto.ChatId && cm.UserId == dto.UserId);

            if (yaExiste)
            {
                return BadRequest("El usuario ya está en este chat.");
            }
            var entidad = new ChatMember
            {
                ChatId = dto.ChatId,
                UserId = dto.UserId,
                IsModerator = dto.IsModerator,
                CanWrite = dto.CanWrite,
                JoinedAt = DateTime.UtcNow
            };

            var id = await chatMemberRepositorio.Insert(entidad);
            entidad.Id = (int)id;

            return CreatedAtAction(nameof(InsertMiembro), new { id = entidad.Id }, entidad);
            //return Ok(entidad);
        }

        [HttpPut("por-id/{Id}")]
        public async Task<IActionResult> UpdateChatMember(long id, ChatMember updatedMember)
        {
            var member = await context.ChatMembers.FindAsync(id);
            if (member == null)
            {
                return NotFound("Miembro no encontrado.");
            }
            if (id != updatedMember.Id)
            {
                return BadRequest("el id del miembro no existe.");
            }
            context.Entry(updatedMember).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            
            {
                if (!ChatMemberExists(id))
                {
                    return NotFound("Miembro no encontrado.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }

        [HttpDelete("por-id/{Id}")]
        public async Task<IActionResult> DeleteChatMember(int id)
        {
            var member = await chatMemberRepositorio.Delete(id);
            //var member = await context.ChatMembers.FindAsync(id);
            if (member == false)
            {
                return NotFound("Miembro no encontrado.");
            }
            return Ok("Miembro eliminado correctamente.");
            //context.ChatMembers.Remove(member);
            //await context.SaveChangesAsync();
            //return NoContent();
        }

        private bool ChatMemberExists(long id)
        {
            return context.ChatMembers.Any(e => e.Id == id);
        }

        #endregion

        #region

        [HttpGet("por-chat/{Chat}")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> GetChatMembersByChat(long chatId)
        {
            var members = await context.ChatMembers
                .Include(cm => cm.User) // Incluye la entidad User relacionada
                .Where(cm => cm.ChatId == chatId)
                .ToListAsync();
            if (members == null || members.Count == 0)
            {
                return NotFound("No members found for this chat.");
            }
            return Ok(members);
        }

        [HttpPut("por-chat/{Chat}")]
        public async Task<IActionResult> UpdateChatMemberByChat(long id, ChatMember chatMember)
        {
            if (id != chatMember.Id)
            {
                return BadRequest("El ID del miembro del chat no coincide.");
            }
            context.Entry(chatMember).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatMemberExists(id))
                {
                    return NotFound("Miembro de chat no encontrado.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        #endregion

        #region
        [HttpGet("por-usuario/{UserId}")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> GetChatMembersByUser(long userId)
        {
            var members = await context.ChatMembers
                .Include(cm => cm.Chat) // Incluye la entidad Chat relacionada
                .Where(cm => cm.UserId == userId)
                .ToListAsync();
            if (members == null || members.Count == 0)
            {
                return NotFound("No members found for this user.");
            }
            return Ok(members);
        }
        #endregion

        #region
        [HttpGet("por-chat-usuario/{User}")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> GetChatMembersByChatAndUser(long chatId, long userId)
        {
            var members = await context.ChatMembers
                .Include(cm => cm.User) // Incluye la entidad User relacionada
                .Where(cm => cm.ChatId == chatId && cm.UserId == userId)
                .ToListAsync();
            if (members == null || members.Count == 0)
            {
                return NotFound("No members found for this chat and user.");
            }
            return Ok(members);
        }

        #endregion

        #region
        [HttpGet("por-moderador/{IsModerator}")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> GetChatMembersByModerator(bool isModerator)
        {
            var members = await context.ChatMembers
                .Include(cm => cm.User) // Incluye la entidad User relacionada
                .Where(cm => cm.IsModerator == isModerator)
                .ToListAsync();
            if (members == null || members.Count == 0)
            {
                return NotFound("No members found with the specified moderator status.");
            }
            return Ok(members);
        }
        #endregion

        #region
        [HttpGet("por-escritura/{CanWrite}")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> GetChatMembersByCanWrite(bool canWrite)
        {
            var members = await context.ChatMembers
                .Include(cm => cm.User) // Incluye la entidad User relacionada
                .Where(cm => cm.CanWrite == canWrite)
                .ToListAsync();
            if (members == null || members.Count == 0)
            {
                return NotFound("No members found with the specified write permission.");
            }
            return Ok(members);
        }

        #endregion
    }

}
