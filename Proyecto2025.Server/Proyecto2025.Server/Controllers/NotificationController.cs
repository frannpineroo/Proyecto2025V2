using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Server.Hubs;
using Proyecto2025.Shared.DTO;
using System;

[ApiController]
[Route("api/Notification")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationRepositorio _notificacionRepo;
    private readonly AppDbContext _context;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationsController(
        AppDbContext context,
        INotificationRepositorio notificacionRepo,
        IHubContext<NotificationHub> hubContext
    )
    {
        _notificacionRepo = notificacionRepo;
        _context = context;
        _hubContext = hubContext;
    }

    // ------------------------------
    // OBTENER NOTIFICACIONES PENDIENTES
    // ------------------------------
    [HttpGet("user/{userId}/pending")]
    public async Task<ActionResult<List<NotificationDTO>>> GetPendingByUser(int userId)
    {
        try
        {
            var notifications = await _notificacionRepo.GetPendingByUserAsync(userId);
            return Ok(notifications);
        }
        catch
        {
            return StatusCode(500, "Error inesperado al obtener las notificaciones.");
        }
    }

    // ------------------------------
    // CONTAR PENDIENTES
    // ------------------------------
    [HttpGet("count/{userId}")]
    public async Task<ActionResult<int>> GetCount(int userId)
    {
        var list = await _notificacionRepo.GetPendingByUserAsync(userId);
        return Ok(list.Count);
    }

    // ------------------------------
    // MARCAR COMO LEÍDA
    // ------------------------------
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

            // 🔵 OPCIÓN 1 (si no tenés userId en el repo)
            // Avisar a TODOS que se actualice el badge
            await _hubContext.Clients.All.SendAsync("NotificationUpdated");

            // 🔵 OPCIÓN 2 (ideal si tenés userId de esa notificación)
            // await _hubContext.Clients.User(userId.ToString())
            //     .SendAsync("NotificationUpdated");

            return NoContent();
        }
        catch
        {
            return StatusCode(500, "Error inesperado al marcar la notificación como leída.");
        }
    }

    // ------------------------------
    // CREAR NOTIFICACIÓN (TEST)
    // ------------------------------
    [HttpPost("send-test")]
    public async Task<IActionResult> Post([FromBody] NotificationDTO dto)
    {
        var notificationEntity = new Notification
        {
            Message = dto.Message,
            CreatedAt = DateTime.Now,
            IsPending = true,
            UserId = dto.UserId
        };

        await _context.Notifications.AddAsync(notificationEntity);
        await _context.SaveChangesAsync();

        // 🔵 ENVIAR NOTIFICACIÓN EN TIEMPO REAL SOLO A ESE USER
        await _hubContext.Clients.User(dto.UserId.ToString())
            .SendAsync("ReceiveNotification", new
            {
                id = notificationEntity.Id,
                message = notificationEntity.Message,
                createdAt = notificationEntity.CreatedAt
            });

        return Ok(new
        {
            Id = notificationEntity.Id,
            Message = "Notificación registrada con éxito."
        });
    }
}
