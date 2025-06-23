using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/Role")]
    public class RoleController : ControllerBase
    {


        private readonly AppDbContext context;

        public RoleController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetRoles()
        {
            var Roles = await context.Roles.ToListAsync();
            if (Roles == null || Roles.Count == 0)
            {
                return NotFound("No se encuentran roles.");
            }
            return Ok(Roles);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Post(Role DTORole)
        {
            try
            {
                await context.Roles.AddAsync(DTORole);
                await context.SaveChangesAsync();
                return Ok(DTORole.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el rol: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, Role DTORole)
        {
            if (id != DTORole.Id)
            {
                return BadRequest("Datos inválidos.");
            }
            var existeRole = await context.Roles.AnyAsync(x => x.Id == id);
            if (!existeRole)
            {
                return NotFound("El rol no existe.");
            }
            try
            {
                context.Roles.Update(DTORole);
                await context.SaveChangesAsync();
                return Ok("Roles actualizados.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar el rol: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var existeRole = await context.Roles.AnyAsync(x => x.Id == id);
            if (!existeRole)
            {
                return NotFound("El rol no existe.");
            }

            var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            context.Roles.Remove(role);
            await context.SaveChangesAsync();
            return Ok("Rol eliminado.");
        }

    }
}
