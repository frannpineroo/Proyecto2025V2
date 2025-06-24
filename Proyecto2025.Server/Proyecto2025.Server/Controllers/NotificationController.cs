using Microsoft.AspNetCore.Mvc;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Microsoft.EntityFrameworkCore;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("Api/Notification")]

    public class NotificationController : ControllerBase
    {
        private readonly AppDbContext context;
        public NotificationController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Notification>>> GetNotifications()
        {
            var Notification = await context.Notifications.ToListAsync();
            if (Notification == null)
            {
                return NotFound("No se encontraron Notificaciones");
            }
            if (Notification.Count == 0)
            {
                return Ok("No existen Notificaciones por ahora");
            }
            return Ok(Notification);
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Notification>> GetByid(int Id)
        {
            var notification = await context.Notifications.FirstOrDefaultAsync(x => x.Id == Id);
            if (notification is null)
            {
                return NotFound($"No existe la notificacion con id {Id}");
            }
            return Ok(notification);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Notification DTO)
        {
            await context.Notifications.AddAsync(DTO);
            await context.SaveChangesAsync();
            return Ok(DTO.Id);
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var existe = await context.Notifications.AnyAsync(x => x.Id == Id);
            if (existe == false)
            {
                return NotFound($"No Existe la Notificación con el Id: {Id}.");
            }
            var notification = await context.Notifications.FirstOrDefaultAsync(x => x.Id == Id);
            context.Notifications.Remove(notification);
            await context.SaveChangesAsync();
            return Ok($"Notificación con el Id {Id} eliminada correctamente");
        }
        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int Id, Notification DTO)
        {
            if
                (Id != DTO.Id)
            {
                return BadRequest("Datos no validos");
            }
            var existe = await context.Notifications.AnyAsync(x => x.Id == Id);
            if (existe!)
            {
                return NotFound($"No existe la notificacion con id {Id}");
            }
            context.Update(DTO);
            await context.SaveChangesAsync();
            return Ok($"Notificacion con id {Id} actualizada correctamente");
        }
    }
}
