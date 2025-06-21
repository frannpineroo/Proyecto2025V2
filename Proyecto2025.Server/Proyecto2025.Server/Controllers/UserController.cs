using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext context;

        public UserController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            var users = await context.Users.ToListAsync();
            if (users == null)
            {
                return NotFound("No se encontraron los usuarios cargados.");
            }
            if (users.Count == 0)
            {
                return Ok("No hay usuarios cargados");
            }

            return Ok(users);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<User>> GetById(long id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return NotFound("El usuario no existe.");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Post(User DTO)
        {
            try
            {
                await context.Users.AddAsync(DTO);
                await context.SaveChangesAsync();
                return Ok(DTO.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el usuario: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, User DTO)
        {
            if (id != DTO.Id)
            {
                return BadRequest("Datos invalidos.");
            }
            var existe = await context.Users.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("El usuario no existe.");
            }
            context.Entry(DTO).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
                return Ok("Usuario actualizado correctamente.");
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest($"Error al actualizar el usuario: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var existe = await context.Users.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("El usuario no existe.");
            }

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return Ok("Usuario eliminado correctamente.");
        }
    }
}
