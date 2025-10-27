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
    public class ChatMembersController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IChatMembersRepositorio<ChatMembers> chatMemberRepositorio;

        public ChatMembersController(AppDbContext context,
            IChatMembersRepositorio<ChatMembers> chatMemberRepositorio)
        {
            this.context = context;
            this.chatMemberRepositorio = chatMemberRepositorio;
        }
        #region
        [HttpGet("por-id/{Id}")]
        public async Task<ActionResult<IEnumerable<ChatMembers>>> GetChatMembers(long chatId)
        {
            // Inicialización simplificada y con miembros requeridos
            ChatMembers member = new ChatMembers
            {
                Id = (int)chatId,
                ChatId = chatId,
                UserId = 1, // Asigna un valor válido a UserId
                IsModerator = false,
                CanWrite = true,
                JoinedAt = System.DateTime.UtcNow
            };
            //await context.ChatMembers.AddAsync(member);
            //await context.SaveChangesAsync();
            //var members = await context.ChatMembers
            var members = await chatMemberRepositorio.SelectByChatId(chatId);
                //.Include(cm => cm.User) // Incluye la entidad User relacionada
                //.Where(cm => cm.ChatId == chatId)
                //.ToListAsync();
            if (members == null || members.Count == 0)
            {
                return NotFound("No members found for this chat.");
            }
            return Ok(members);
        }
        [HttpPost("aceptar")]
        public async Task<ActionResult<ChatMembers>> InsertMiembro([FromBody] ChatMembersDTO dto)
        {
            var entidad = new ChatMembers
            {
                ChatId = dto.ChatId,
                UserId = dto.UserId,
                IsModerator = dto.IsModerator,
                CanWrite = dto.CanWrite,
                JoinedAt = DateTime.UtcNow
            };

            var id = await chatMemberRepositorio.Insert(entidad);
            entidad.Id = (int)id;

            return Ok(entidad);
        }



        [HttpPut("por-id/{Id}")]
        public async Task<IActionResult> UpdateChatMember(long id, ChatMembers updatedMember)
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
        public async Task<ActionResult<IEnumerable<ChatMembers>>> GetChatMembersByChat(long chatId)
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
        public async Task<IActionResult> UpdateChatMemberByChat(long id, ChatMembers chatMember)
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
        public async Task<ActionResult<IEnumerable<ChatMembers>>> GetChatMembersByUser(long userId)
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
        public async Task<ActionResult<IEnumerable<ChatMembers>>> GetChatMembersByChatAndUser(long chatId, long userId)
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
        public async Task<ActionResult<IEnumerable<ChatMembers>>> GetChatMembersByModerator(bool isModerator)
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
        public async Task<ActionResult<IEnumerable<ChatMembers>>> GetChatMembersByCanWrite(bool canWrite)
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
