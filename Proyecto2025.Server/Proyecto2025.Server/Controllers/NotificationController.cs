using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Shared.DTO;
using System;

[ApiController]
[Route("api/Notification")]
public class NotificationsController : ControllerBase
{
    private readonly INotificacionRepositorio _notificacionRepo;
    private readonly AppDbContext context;


    public NotificationsController(AppDbContext context, INotificacionRepositorio notificacionRepo)
    {
        _notificacionRepo = notificacionRepo;
        this.context = context;
    }


    [HttpGet("user/{userId}/pending")]
    public async Task<ActionResult<List<NotificationDTO>>> GetPendingByUser(int userId)
    {
        try
        {
            var notifications = await _notificacionRepo.GetPendingByUserAsync(userId);

            return Ok(notifications);
        }
        catch (Exception ex)
        {

            return StatusCode(500, "Error inesperado al obtener las notificaciones.");
        }
    }

    [HttpPut("{notificationId}/markasread")]
    public async Task<IActionResult> MarkAsRead(long notificationId)
    {
        try
        {
            var exito = await _notificacionRepo.MarkAsReadAsync(notificationId);

            if (!exito)
            {

                return NotFound($"No existe la notificación con el Id: {notificationId}.");
            }


            return NoContent();
        }
        catch (Exception ex)
        {

            return StatusCode(500, "Error inesperado al marcar la notificación como leída.");
        }
    }

    [HttpPost("send-test")]
    public async Task<IActionResult> Post([FromBody] NotificationDTO dto)
    {

        var notificationEntity = new Notification
        {
            Message = dto.Message,
            CreatedAt = DateTime.Now,
            IsPending = true,
            UserId = 1
        };

        await context.Notifications.AddAsync(notificationEntity);
        await context.SaveChangesAsync();

        return Ok(new { Id = notificationEntity.Id, Message = "Notificación registrada con éxito." });
    }
}
