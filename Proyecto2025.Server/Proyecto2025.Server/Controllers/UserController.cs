using Microsoft.AspNetCore.Mvc;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IRepositorio<Chat> chatRepositorio;
        private readonly IChatMemberRepositorio<ChatMember> chatMemberRepositorio;
        //private readonly AppDbContext context;

        public UserController(IUsuarioRepositorio usuarioRepositorio,
                              IRepositorio<Chat> chatRepositorio,
                              IChatMemberRepositorio<ChatMember> chatMemberRepositorio)
                              
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.chatRepositorio = chatRepositorio;
            this.chatMemberRepositorio = chatMemberRepositorio;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            try
            {
                var users = await usuarioRepositorio.ObtenerTodosLosUsuariosAsync();
                if (users == null || users.Count == 0)
                {
                    return NotFound("No se encontraron los usuarios cargados.");
                }
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al obtener los usuarios: {e.Message}");
            }
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> GetById(long id)
        {
            try
            {
                var user = await usuarioRepositorio.ObtenerUsuarioPorIdAsync(id);

                if (user == null)
                {
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener usuario: {ex.Message}");
            }
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<List<ListaUsuarioDTO>>> GetUsers(string filtro)
        {
            try
            {
                var users = await usuarioRepositorio.SelectUsuarios(filtro);
                if (users == null || users.Count == 0)
                {
                    return NotFound("No se encontraron los usuarios cargados.");
                }
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al obtener los usuarios: {e.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(long), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<long>> CrearUsuario([FromBody] CrearUsuarioDTO dto)
        {
            try
            {
                // Verificar si el email ya existe
                var emailExiste = await usuarioRepositorio.ExisteEmailAsync(dto.Email);
                if (emailExiste)
                {
                    return BadRequest("Ya existe un usuario con este email.");
                }

                // Crear usuario
                var usuario = await usuarioRepositorio.CrearUsuarioAsync(dto);

                return Ok(usuario.Id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear usuario: {ex.Message}");
            }
        }

        //AGREGADO POR EZEQUIEL. POR Q DE ACUERDO A USUARIO Q INICIA SECCION TIENE Q CARGAR DESDE EL PRINCIPIO EN CHATS
        //PARA Q USUARIOS TENGA SUS CHATS Y GRUPOS PROPIOS
        //[HttpPost("registrar")]
        //public async Task<ActionResult> Registrar([FromBody] CrearUsuarioDTO dto)
        //{
        //    try
        //    {
        //        // Crear usuario usando el repo existente
        //        var user = await usuarioRepositorio.CrearUsuarioAsync(dto);

        //        // Crear chat automático
        //        var chat = new Chat
        //        {
        //            Name = $"{user.FirstName} {user.LastName}",
        //            IsGroup = false,
        //            IsModerated = false,
        //            CreatedAt = DateTime.UtcNow,
        //            UpdatedAt = DateTime.UtcNow
        //        };

        //        await chatRepositorio.Insert(chat);

        //        // Crear ChatMember
        //        var member = new ChatMember
        //        {
        //            ChatId = chat.Id,
        //            UserId = user.Id,
        //            IsModerator = true,
        //            CanWrite = true,
        //            JoinedAt = DateTime.UtcNow
        //        };

        //        await chatMemberRepositorio.Insert(member);

        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest($"Error al registrar y crear chat: {e.Message}");
        //    }
        //}

        [HttpPut("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Put(long id, [FromBody] User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest("El ID del parámetro no coincide con el ID del usuario.");
                }

                var actualizado = await usuarioRepositorio.ActualizarUsuarioAsync(id, user);

                if (!actualizado)
                {
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }

                return Ok("Usuario actualizado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar usuario: {ex.Message}");
            }
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var eliminado = await usuarioRepositorio.EliminarUsuarioAsync(id);

                if (!eliminado)
                {
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }

                return Ok("Usuario eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar usuario: {ex.Message}");
            }
        }
    }
}
