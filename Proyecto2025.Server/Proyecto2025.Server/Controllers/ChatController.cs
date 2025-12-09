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
    [Route("api/Chat")]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IRepositorio<Chat> repositorio;

        public ChatController(AppDbContext context,
                              IRepositorio<Chat> repositorio)

        {
            this.context = context;
            this.repositorio = repositorio;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<Chat>>> GetAll()
        //{
        //    var chats = await repositorio.Select();
        //    if (chats == null)
        //    {
        //        return NotFound("No se encontraron chats, verifique de nuevo.");
        //    }
        //    //mapeo de entidades a DTOs
        //    //var listaDTO = chats.Select(chat => new ListaChatsDTO
        //    //{
        //    //    Id = chat.Id,
        //    //    Name = chat.Name,
        //    //    IsGroup = chat.IsGroup,
        //    //    IsModerated = chat.IsModerated,
        //    //    CreatedAt = chat.CreatedAt,
        //    //    UpdatedAt = chat.UpdatedAt,
        //    //}).ToList();

        //    return Ok(chats);
        //}

        //[HttpGet("{id}/chats")] //chats de usuario
        //public async Task<ActionResult<List<ListaChatDTO>>> GetChats(long id)
        //{

        //    var chats = await context.ChatMembers
        //    .Where(cm => cm.UserId == id)
        //    .Join(context.Chats,
        //            cm => cm.ChatId,
        //            ch => ch.Id,
        //            (cm, ch) => ch).ToListAsync();

        //    if (chats == null)
        //    {
        //        return NotFound("No se encontraron chats, verifique de nuevo.");
        //    }
        //    return Ok(chats);
        //}

        //mio

        //[HttpGet]
        //public async Task<ActionResult<List<ChatDTO>>> GetChats()
        //{
        //    var chats = await context.Chats
        //        .Select(c => new ChatDTO
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            UltimoMensaje = context.Messages
        //                .Where(m => m.ChatId == c.Id)
        //                .OrderByDescending(m => m.SentAt)
        //                .Select(m => m.Content)
        //                .FirstOrDefault() ?? "Sin mensajes"
        //        })
        //        .ToListAsync();

        //    return Ok(chats);
        //}

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ChatDTO>> GetChat(int id)
        {
            var chat = await context.Chats
                .Where(c => c.Id == id)
                .Select(c => new ChatDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsGroup = c.IsGroup,
                    IsModerated = c.IsModerated,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    UltimoMensaje = context.Messages
                        .Where(m => m.ChatId == c.Id)
                        .OrderByDescending(m => m.SentAt)
                        .Select(m => m.Content)
                        .FirstOrDefault() ?? "Sin mensajes"
                })
                .FirstOrDefaultAsync();

            if (chat == null)
            {
                return NotFound("El chat no existe");
            }

            return Ok(chat);
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatDTO>>> GetTodos()
        {
            var chats = await context.Chats
                .Select(c => new ChatDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsGroup = c.IsGroup,
                    IsModerated = c.IsModerated,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    UltimoMensaje = context.Messages
                        .Where(m => m.ChatId == c.Id)
                        .OrderByDescending(m => m.SentAt)
                        .Select(m => m.Content)
                        .FirstOrDefault() ?? "Sin mensajes"
                })
                .ToListAsync();

            return Ok(chats);
        }


        [HttpPost("crear")]
        public async Task<ActionResult<string>> CrearChat(CrearChatDTO dto)
        {
            if (dto.UserIds == null || dto.UserIds.Count == 0)
                return BadRequest("Debe seleccionar al menos un usuario.");

            var chat = new Chat
            {
                Name = dto.Name,
                IsGroup = dto.IsGroup,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Chats.Add(chat);
            await context.SaveChangesAsync();

            // Relación Chat - Usuarios
            foreach (var userId in dto.UserIds)
            {
                context.ChatMembers.Add(new ChatMember
                {
                    ChatId = chat.Id,
                    UserId = userId
                });
            }

            await context.SaveChangesAsync();

            return Ok("Chat creado correctamente");
        }























        #region
        [HttpGet("por-chat-lista/{id}")]//listachats
        public async Task<ActionResult<List<ListaChatDTO>>> GetChatslista(long id)
        {
            var chats = await repositorio.SelectById(id);
            //var chats = await context.Chats.ToListAsync();
            if (chats == null)
            {
                return NotFound("No se encontraron chats, verifique de nuevo.");
            }
            return Ok(chats);
        }

        [HttpGet("{id}/{filtro}")]
        public async Task<ActionResult<List<ListaChatDTO>>> GetChats(long id, string filtro)
        {
            //try
            //{
            //    return await context.Chats
            //    .Where(u => u.Name.ToLower().Contains(filtro)).ToListAsync();
            //
            //    
            //}
            //catch (Exception e)
            //{
            //    return BadRequest($"Error al obtener los usuarios: {e.Message}");
            //}
            var chats = await context.ChatMembers
            .Where(cm => cm.UserId == id)
            .Join(context.Chats,
                    cm => cm.ChatId,
                    ch => ch.Id,
                    (cm, ch) => ch)
            .Where(ch => ch.Name.ToLower().Contains(filtro.ToLower()))
            .ToListAsync();

            if (chats.Count == 0)
            {
                return NotFound("No se encontraron chats, verifique de nuevo.");
            }
            return Ok(chats);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Post([FromBody]ChatDTO DTO)
        {
            try
            {
                Chat chat = new()
                {
                    Id = DTO.Id, // Soluciona CS9035: Id es requerido
                    Name = DTO.Name,
                    IsGroup = DTO.IsGroup,
                    IsModerated = DTO.IsModerated,
                    CreatedAt = DTO.CreatedAt,
                    UpdatedAt = DTO.UpdatedAt,
                    
                };
                var id = await repositorio.Insert(chat);
                return Ok(chat.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el chat: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Chat DTO)
        {
            if (id != DTO.Id)
            {
                return BadRequest("Datos inválidos, vuelva a verificar los datos");
            }
            //var existe = await repositorio.existe((int)id);
            //var existe = await context.Chats.AnyAsync(x => x.Id == id);
            //if (!existe)
            //{
            //    return NotFound("El chat no existe.");
            //}
            //try
            //{
            //    context.Chats.Update(DTO);
            //    await context.SaveChangesAsync();
            //    return Ok();
            //}
            //catch (Exception e)
            var existe = await repositorio.Update(id, DTO);
            {
                if (!existe)
                {
                    return NotFound("El chat no existe.");
                }
                return Ok($"Error al actualizar el chat: {id} actualizado con exito");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
           
            var chat = await context.Chats.FirstOrDefaultAsync(c => c.Id == id);
            //var chat = await context.Chats.FindAsync(id);
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
        #endregion

    }
}
 