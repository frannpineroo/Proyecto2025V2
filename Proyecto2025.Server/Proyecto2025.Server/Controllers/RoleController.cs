using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/Role")]
    public class RoleController : ControllerBase
    {


        private readonly IRepositorio<Role> repositorio;

        public RoleController(IRepositorio<Role> repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<RolDTO>>> GetRoles()
        {
            var Roles = await repositorio.Select();
            if (Roles == null || Roles.Count == 0)
            {
                return NotFound("No se encuentran roles.");
            }
            return Ok(Roles);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Post(RolDTO Rol)
        {
            try
            {
                Role entidad = new Role
                {
                    Id = Rol.Id,
                    Name = Rol.Name
                };
                var id = await repositorio.Insert(entidad);
                return Ok("El rol ha sido creado exitosamente");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el rol: {e.Message}");
            }
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, Role Rol)
        {
            
            try
            {
                await repositorio.Update(id, Rol);
                //if (id != Rol.Id)
                //{
                //   return BadRequest("Datos inválidos.");
                //}
                //var existe = await repositorio.Update(id, Rol);
                //if (!existe)
                //{
                //    return NotFound("El rol no existe.");
                //}
                return Ok("Roles actualizados.");
            }
            catch (Exception e)
            {
                return BadRequest($"Error al actualizar el rol: {e.Message}");
            }

        }

       

    }
}
