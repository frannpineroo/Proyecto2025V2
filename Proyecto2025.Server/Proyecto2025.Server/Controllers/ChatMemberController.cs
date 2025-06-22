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

        [HttpPost("por-id/{Id}")]
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
        [HttpGet("por chatid/{ChatId}")]
        public IActionResult GetChatMember(long id)
        {
            var chatMember = context.ChatMembers.FirstOrDefault(cm => cm.Id == id);
            if (chatMember == null)
            {
                return NotFound("Miembro de chat no encontrado.");
            }
            return Ok(chatMember);
        }

        [HttpPost("por-chatid/{ChatId}")]
        public async Task<ActionResult<ChatMember>> CreateChatMember(ChatMember chatMember)
        {
            if (chatMember == null)
            {
                return BadRequest("El miembro del chat no puede ser nulo.");
            }
            context.ChatMembers.Add(chatMember);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetChatMember), new { id = chatMember.Id }, chatMember);
        }
        [HttpPut("por-chatid/{ChatId}")]
        public async Task<IActionResult> UpdateChatMemberChatId(long id, ChatMember chatMember)
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
        [HttpDelete("por-chatid/{ChatId}")]
        public async Task<IActionResult> DeleteChatMemberChatId(long id)
        {
            var chatMember = await context.ChatMembers.FindAsync(id);
            if (chatMember == null)
            {
                return NotFound("Miembro de chat no encontrado.");
            }
            context.ChatMembers.Remove(chatMember);
            await context.SaveChangesAsync();
            return NoContent();
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

        [HttpPost("por-chat/{Chat}")]
        public async Task<ActionResult<ChatMember>> CreateChatMemberByChat(ChatMember chatMember)
        {
            if (chatMember == null)
            {
                return BadRequest("El miembro del chat no puede ser nulo.");
            }
            context.ChatMembers.Add(chatMember);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetChatMembersByChat), new { chatId = chatMember.ChatId }, chatMember);
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

        [HttpDelete("por-chat/{Chat}")]
        public async Task<IActionResult> DeleteChatMemberByChat(long id)
        {
            var chatMember = await context.ChatMembers.FindAsync(id);
            if (chatMember == null)
            {
                return NotFound("Miembro de chat no encontrado.");
            }
            context.ChatMembers.Remove(chatMember);
            await context.SaveChangesAsync();
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

        [HttpPost("por-usuario/{UserId}")]
        public async Task<ActionResult<ChatMember>> CreateChatMemberByUser(ChatMember chatMember)
        {
            if (chatMember == null)
            {
                return BadRequest("El miembro del chat no puede ser nulo.");
            }
            context.ChatMembers.Add(chatMember);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetChatMembersByUser), new { userId = chatMember.UserId }, chatMember);
        }

        [HttpPut("por-usuario/{UserId}")]

        public async Task<IActionResult> UpdateChatMemberByUser(long id, ChatMember chatMember)
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

        [HttpDelete("por-usuario/{UserId}")]
        public async Task<IActionResult> DeleteChatMemberByUser(long id)
        {
            var chatMember = await context.ChatMembers.FindAsync(id);
            if (chatMember == null)
            {
                return NotFound("Miembro de chat no encontrado.");
            }
            context.ChatMembers.Remove(chatMember);
            await context.SaveChangesAsync();
            return NoContent();
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

        [HttpPost("por-chat-usuario/{User}")]
        public async Task<ActionResult<ChatMember>> CreateChatMemberByChatAndUser(ChatMember chatMember)
        {
            if (chatMember == null)
            {
                return BadRequest("El miembro del chat no puede ser nulo.");
            }
            context.ChatMembers.Add(chatMember);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetChatMembersByChatAndUser), new { chatId = chatMember.ChatId, userId = chatMember.UserId }, chatMember);
        }

        [HttpPut("por-chat-usuario/{User}")]

        public async Task<IActionResult> UpdateChatMemberByChatAndUser(long id, ChatMember chatMember)
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

        [HttpDelete("por-chat-usuario/{User}")]
        public async Task<IActionResult> DeleteChatMemberByChatAndUser(long id)
        {
            var chatMember = await context.ChatMembers.FindAsync(id);
            if (chatMember == null)
            {
                return NotFound("Miembro de chat no encontrado.");
            }
            context.ChatMembers.Remove(chatMember);
            await context.SaveChangesAsync();
            return NoContent();
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

        [HttpPost("por-moderador/{IsModerator}")]
        public async Task<ActionResult<ChatMember>> CreateChatMemberByModerator(ChatMember chatMember)
        {
            if (chatMember == null)
            {
                return BadRequest("El miembro del chat no puede ser nulo.");
            }
            context.ChatMembers.Add(chatMember);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetChatMembersByModerator), new { isModerator = chatMember.IsModerator }, chatMember);
        }

        [HttpPut("por-moderador/{IsModerator}")]
        public async Task<IActionResult> UpdateChatMemberByModerator(long id, ChatMember chatMember)
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

        [HttpDelete("por-moderador/{IsModerator}")]
        public async Task<IActionResult> DeleteChatMemberByModerator(long id)
        {
            var chatMember = await context.ChatMembers.FindAsync(id);
            if (chatMember == null)
            {
                return NotFound("Miembro de chat no encontrado.");
            }
            context.ChatMembers.Remove(chatMember);
            await context.SaveChangesAsync();
            return NoContent();
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

        [HttpPost("por-escritura/{CanWrite}")]
        public async Task<ActionResult<ChatMember>> CreateChatMemberByCanWrite(ChatMember chatMember)
        {
            if (chatMember == null)
            {
                return BadRequest("El miembro del chat no puede ser nulo.");
            }
            context.ChatMembers.Add(chatMember);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetChatMembersByCanWrite), new { canWrite = chatMember.CanWrite }, chatMember);
        }

        [HttpPut("por-escritura/{CanWrite}")]
        public async Task<IActionResult> UpdateChatMemberByCanWrite(long id, ChatMember chatMember)
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

        [HttpDelete("por-escritura/{CanWrite}")]
        public async Task<IActionResult> DeleteChatMemberByCanWrite(long id)
        {
            var chatMember = await context.ChatMembers.FindAsync(id);
            if (chatMember == null)
            {
                return NotFound("Miembro de chat no encontrado.");
            }
            context.ChatMembers.Remove(chatMember);
            await context.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region
        [HttpGet("por-cuandoseunio/{JoinedAt}")]
        public async Task<ActionResult<IEnumerable<ChatMember>>> GetChatMembersByJoinedAt(DateTime joinedAt)
        {
            var members = await context.ChatMembers
                .Include(cm => cm.User) // Incluye la entidad User relacionada
                .Where(cm => cm.JoinedAt.Date == joinedAt.Date)
                .ToListAsync();
            if (members == null || members.Count == 0)
            {
                return NotFound("No members found with the specified join date.");
            }
            return Ok(members);
        }

        [HttpPost("por-cuandoseunio/{JoinedAt}")]
        public async Task<ActionResult<ChatMember>> CreateChatMemberByJoinedAt(ChatMember chatMember)
        {
            if (chatMember == null)
            {
                return BadRequest("El miembro del chat no puede ser nulo.");
            }
            context.ChatMembers.Add(chatMember);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetChatMembersByJoinedAt), new { joinedAt = chatMember.JoinedAt }, chatMember);
        }

        [HttpPut("por-cuandoseunio/{JoinedAt}")]
        public async Task<IActionResult> UpdateChatMemberByJoinedAt(long id, ChatMember chatMember)
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

        [HttpDelete("por-cuandoseunio/{JoinedAt}")]
        public async Task<IActionResult> DeleteChatMemberByJoinedAt(long id)
        {
            var chatMember = await context.ChatMembers.FindAsync(id);
            if (chatMember == null)
            {
                return NotFound("Miembro de chat no encontrado.");
            }
            context.ChatMembers.Remove(chatMember);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
        #endregion

}
