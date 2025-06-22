using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/ChatMember")]
    public class ChatMemberController : ControllerBase
    {
        private readonly AppDbContext context;
        public ChatMemberController(AppDbContext context)
        {
            this.context = context;
        }

        #region
        [HttpGet("por-id/{Id}")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> GetChatMembers(long chatId)
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

        [HttpPost]
        public async Task<ActionResult<ChatMember>> GetChatMemberById(long id)
        {
            var member = await context.ChatMembers
                .Include(cm => cm.User) // Incluye la entidad User relacionada
                .FirstOrDefaultAsync(cm => cm.Id == id);
            if (member == null)
            {
                return NotFound("Member not found.");
            }
            return Ok(member);
        }

        [HttpPut("por-id/{Id}")]
        public async Task<IActionResult> UpdateChatMember(long id, ChatMember updatedMember)
        {
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
        public async Task<IActionResult> DeleteChatMember(long id)
        {
            var member = await context.ChatMembers.FindAsync(id);
            if (member == null)
            {
                return NotFound("Miembro no encontrado.");
            }
            context.ChatMembers.Remove(member);
            await context.SaveChangesAsync();
            return NoContent();
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
