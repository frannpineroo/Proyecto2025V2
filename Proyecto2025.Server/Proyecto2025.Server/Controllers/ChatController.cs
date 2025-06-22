    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Proyecto2025.BD.Datos;
    using Proyecto2025.BD.Datos.Entity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/Chat")]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext context;

        public ChatController(AppDbContext context)
        {
            this.context = context;
        }

        #region
        [HttpGet("por-id/{id}")]
        public async Task<ActionResult<List<Chat>>> GetChatsById(long id)
        {
            var chats = await context.Chats.ToListAsync();
            if (chats == null || chats.Count == 0)
            {
                return NotFound("No se encontraron chats, verifique de nuevo.");
            }
            return Ok(chats);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Post(Chat DTO)
        {
            try
            {
                await context.Chats.AddAsync(DTO);
                await context.SaveChangesAsync();
                return Ok(DTO.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el chat: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, Chat DTO)
        {
            if (id != DTO.Id)
            {
                return BadRequest("Datos inválidos, vuelva a verificar los datos");
            }
            var existe = await context.Chats.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("El chat no existe.");
            }
            try
            {
                context.Chats.Update(DTO);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar el chat: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var chat = await context.Chats.FindAsync(id);
            if (chat == null)
            {
                return NotFound("El chat no existe.");
            }
            try
            {
                context.Chats.Remove(chat);
                await context.SaveChangesAsync();
                return Ok("Chat eliminado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al eliminar el chat: {e.Message}");
            }

        }
        #endregion

        #region
        [HttpGet("por-nombre/{Name}")]
        public async Task<ActionResult<Chat>> GetChatByName(string Name)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.Name == Name);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            return Ok(chat);
        }

        [HttpPost("{Name}")]
        public async Task<ActionResult<long>> PostChatByName(string Name, Chat DTO)
        {
            try
            {
                DTO.Name = Name; // Asignar el nombre al DTO
                await context.Chats.AddAsync(DTO);
                await context.SaveChangesAsync();
                return Ok(DTO.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el chat: {e.Message}");
            }
        }

        [HttpPut("por-nombre/{Name}")]

        public async Task<ActionResult> PutChatByName(string Name, Chat DTO)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.Name == Name);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            try
            {
                chat.Name = DTO.Name; // Actualizar el nombre del chat
                chat.Messages = DTO.Messages; // Actualizar los mensajes del chat
                context.Chats.Update(chat);
                await context.SaveChangesAsync();
                return Ok("Chat actualizado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar el chat: {e.Message}");
            }
        }

        [HttpDelete("por-nombre/{Name}")]
        public async Task<ActionResult> DeleteChatByName(string Name)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.Name == Name);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            try
            {
                context.Chats.Remove(chat);
                await context.SaveChangesAsync();
                return Ok("Chat eliminado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al eliminar el chat: {e.Message}");
            }
        }
        #endregion


        #region
        [HttpGet("por-grupo/{IsGroup}")]

        public async Task<ActionResult<List<Chat>>> GetChatsByGroupStatus(bool IsGroup)
        {
            var chats = await context.Chats.Where(c => c.IsGroup == IsGroup).ToListAsync();
            if (chats == null || chats.Count == 0)
            {
                return NotFound("No se encontraron chats con el estado de grupo especificado.");
            }
            return Ok(chats);
        }


        [HttpPost("por-grupo/{IsGroup}")]
        public async Task<ActionResult<long>> PostChatByGroupStatus(bool IsGroup, Chat DTO)
        {
            try
            {
                DTO.IsGroup = IsGroup; // Asignar el estado de grupo al DTO
                await context.Chats.AddAsync(DTO);
                await context.SaveChangesAsync();
                return Ok(DTO.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el chat: {e.Message}");
            }

        }

        [HttpPut("por-grupo/{IsGroup}")]
        public async Task<ActionResult> PutChatByGroupStatus(bool IsGroup, Chat DTO)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.IsGroup == IsGroup);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            try
            {
                chat.IsGroup = DTO.IsGroup; // Actualizar el estado de grupo del chat
                context.Chats.Update(chat);
                await context.SaveChangesAsync();
                return Ok("Chat actualizado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar el chat: {e.Message}");
            }
        }
        [HttpDelete("por-grupo/{IsGroup}")]
        public async Task<ActionResult> DeleteChatByGroupStatus(bool IsGroup)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.IsGroup == IsGroup);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            try
            {
                context.Chats.Remove(chat);
                await context.SaveChangesAsync();
                return Ok("Chat eliminado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al eliminar el chat: {e.Message}");
            }
        }
        #endregion


        #region
        [HttpGet("por-moderador/{IsModerated}")]
        public async Task<ActionResult<List<Chat>>> GetChatsByModerationStatus(bool IsModerated)
        {
            var chats = await context.Chats.Where(c => c.IsModerated == IsModerated).ToListAsync();
            if (chats == null || chats.Count == 0)
            {
                return NotFound("No se encontraron chats con el estado de moderación especificado.");
            }
            return Ok(chats);
        }

        [HttpPost("por-moderador/{IsModerated}")]

        public async Task<ActionResult<long>> PostChatByModerationStatus(bool IsModerated, Chat DTO)
        {
            try
            {
                DTO.IsModerated = IsModerated; // Asignar el estado de moderación al DTO
                await context.Chats.AddAsync(DTO);
                await context.SaveChangesAsync();
                return Ok(DTO.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el chat: {e.Message}");
            }
        }

        [HttpPut("por-moderador/{IsModerated}")]

        public async Task<ActionResult> PutChatByModerationStatus(bool IsModerated, Chat DTO)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.IsModerated == IsModerated);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            try
            {
                chat.IsModerated = DTO.IsModerated; // Actualizar el estado de moderación del chat
                context.Chats.Update(chat);
                await context.SaveChangesAsync();
                return Ok("Chat actualizado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar el chat: {e.Message}");
            }
        }

        [HttpDelete("por-moderador/{IsModerated}")]

        public async Task<ActionResult> DeleteChatByModerationStatus(bool IsModerated)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.IsModerated == IsModerated);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            try
            {
                context.Chats.Remove(chat);
                await context.SaveChangesAsync();
                return Ok("Chat eliminado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al eliminar el chat: {e.Message}");
            }

        }
        #endregion

        #region
        [HttpGet("por-fechadecreacion/{CreatedAt}")]
        public async Task<ActionResult<List<Chat>>> GetChatsByCreationDate(DateTime CreatedAt)
        {
            var chats = await context.Chats.Where(c => c.CreatedAt.Date == CreatedAt.Date).ToListAsync();
            if (chats == null || chats.Count == 0)
            {
                return NotFound("No se encontraron chats con la fecha de creación especificada.");
            }
            return Ok(chats);
        }

        [HttpPost("por-fechadecreacion/{CreatedAt}")]
        public async Task<ActionResult<long>> PostChatByCreationDate(DateTime CreatedAt, Chat DTO)
        {
            try
            {
                DTO.CreatedAt = CreatedAt; // Asignar la fecha de creación al DTO
                await context.Chats.AddAsync(DTO);
                await context.SaveChangesAsync();
                return Ok(DTO.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el chat: {e.Message}");
            }
        }
        [HttpPut("por-fechadecreacion/{CreatedAt}")]
        public async Task<ActionResult> PutChatByCreationDate(DateTime CreatedAt, Chat DTO)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.CreatedAt.Date == CreatedAt.Date);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            try
            {
                chat.CreatedAt = DTO.CreatedAt; // Actualizar la fecha de creación del chat
                context.Chats.Update(chat);
                await context.SaveChangesAsync();
                return Ok("Chat actualizado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar el chat: {e.Message}");
            }
        }

        [HttpDelete("por-fechadecreacion/{CreatedAt}")]
        public async Task<ActionResult> DeleteChatByCreationDate(DateTime CreatedAt)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.CreatedAt.Date == CreatedAt.Date);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            try
            {
                context.Chats.Remove(chat);
                await context.SaveChangesAsync();
                return Ok("Chat eliminado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al eliminar el chat: {e.Message}");
            }
        }

        #endregion

        #region
        [HttpGet("por-fechadeactualizacion/{UpdatedAt}")]
        public async Task<ActionResult<List<Chat>>> GetChatsByUpdateDate(DateTime UpdatedAt)
        {
            var chats = await context.Chats.Where(c => c.UpdatedAt.Date == UpdatedAt.Date).ToListAsync();
            if (chats == null || chats.Count == 0)
            {
                return NotFound("No se encontraron chats con la fecha de actualización especificada.");
            }
            return Ok(chats);
        }

        [HttpPost("por-fechadeactualizacion/{UpdatedAt}")]
        public async Task<ActionResult<long>> PostChatByUpdateDate(DateTime UpdatedAt, Chat DTO)
        {
            try
            {
                DTO.UpdatedAt = UpdatedAt; // Asignar la fecha de actualización al DTO
                await context.Chats.AddAsync(DTO);
                await context.SaveChangesAsync();
                return Ok(DTO.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el chat: {e.Message}");
            }
        }
        [HttpPut("por-fechadeactualizacion/{UpdatedAt}")]
        public async Task<ActionResult> PutChatByUpdateDate(DateTime UpdatedAt, Chat DTO)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.UpdatedAt.Date == UpdatedAt.Date);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            try
            {
                chat.UpdatedAt = DTO.UpdatedAt; // Actualizar la fecha de actualización del chat
                context.Chats.Update(chat);
                await context.SaveChangesAsync();
                return Ok("Chat actualizado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar el chat: {e.Message}");
            }
        }

        [HttpDelete("por-fechadeactualizacion/{UpdatedAt}")]
        public async Task<ActionResult> DeleteChatByUpdateDate(DateTime UpdatedAt)
        {
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.UpdatedAt.Date == UpdatedAt.Date);
            if (chat == null)
            {
                return NotFound("Chat no encontrado.");
            }
            try
            {
                context.Chats.Remove(chat);
                await context.SaveChangesAsync();
                return Ok("Chat eliminado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al eliminar el chat: {e.Message}");
            }
        }
        #endregion

        
    }
}
       
 