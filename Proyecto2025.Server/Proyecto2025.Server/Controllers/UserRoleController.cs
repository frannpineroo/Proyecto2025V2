using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.BD.Datos;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/UserRole")]
    public class UserRoleController : ControllerBase
    {
        
        
            private readonly AppDbContext context;

            public UserRoleController(AppDbContext context)
            {
                this.context = context;
            }

            [HttpGet]
            public async Task<ActionResult<List<UserRole>>> GetUserRoles(long idUser, long idRole)
            {
                var UserRoles = await context.UserRoles.FindAsync(idUser,idRole);
                if (UserRoles == null)
                {
                    return NotFound("No se encontraron roles de usuario.");
                }
                return Ok(UserRoles);
            }

            [HttpPost]
            public async Task<ActionResult> Post(UserRole DTOURole)
            {
                try
                {
                    await context.UserRoles.AddAsync(DTOURole);
                    await context.SaveChangesAsync();
                    return Ok((DTOURole.IdUser,DTOURole.IdRole)); 
                }
                catch (Exception e)
                {
                    return BadRequest($"Error al asignar rol al usuario: {e.Message}");
                }
            }

            [HttpPut("{id1}/{id2}")]
            public async Task<ActionResult> Put(long id1,long id2, UserRole DTOURole)
            {
                if (id1 != DTOURole.IdUser || id2 != DTOURole.IdRole)
                {
                    return BadRequest("Datos de usuario y rol inválidos.");
                }
                var existeURol = await context.UserRoles.AnyAsync(x => x.IdUser == id1 && x.IdRole == id2);
                if (!existeURol)
                {
                    return NotFound("El rol de usuario no existe.");
                }
                try
                {
                    context.UserRoles.Update(DTOURole);
                    await context.SaveChangesAsync();
                    return Ok("Rol de usuario actualizado.");
                }
                catch (Exception e)
                {
                    return BadRequest($"Error al actualizar el rol del usuario: {e.Message}");
                }
            }

            [HttpDelete("{idUser}/{idRole}")]
            public async Task<ActionResult> Delete(long idUser, long idRole)
            {
            var existeURole = await context.UserRoles.FindAsync(idUser,idRole);
                if (existeURole == null)
                {
                    return NotFound("El usuario o rol no existe.");
                }


            var existeRole = await context.UserRoles.FirstOrDefaultAsync(x => x.IdUser == idUser && x.IdRole == idRole);
            context.UserRoles.Remove(existeRole);
            await context.SaveChangesAsync();
                return Ok("Rol de usuario eliminado.");
            }
        
    }
}
