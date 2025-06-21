using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/Organizacion")]
    public class OrganizationController : ControllerBase
    {
        private readonly AppDbContext context;

        public OrganizationController(AppDbContext context) 
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Organization>>> GetOrganizations()
        {
            var organizations = await context.Organizations.ToListAsync();
            if (organizations == null || organizations.Count == 0)
            {
                return NotFound("No se encontraron organizaciones.");
            }
            return Ok(organizations);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Post(Organization DTO)
        {
            try
            {
                await context.Organizations.AddAsync(DTO);
                await context.SaveChangesAsync();
                return Ok(DTO.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear la organización: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, Organization DTO)
        {
            if (id != DTO.Id)
            {
                return BadRequest("Datos inválidos.");
            }
            var existe = await context.Organizations.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("La organización no existe.");
            }
            try
            {
                context.Organizations.Update(DTO);
                await context.SaveChangesAsync();
                return Ok("Organización actualizada correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar la organización: {e.Message}");
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
