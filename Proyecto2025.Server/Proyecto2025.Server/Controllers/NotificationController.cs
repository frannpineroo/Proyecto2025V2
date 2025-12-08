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
        // Guardo las dependencias que voy a usar en los endpoints
        _notificacionRepo = notificacionRepo;
        _context = context;
        _hubContext = hubContext;
    }

    // =======================================================
    // OBTENER NOTIFICACIONES PENDIENTES DE UN USUARIO
    // =======================================================
    [HttpGet("user/{userId}/pending")]
    public async Task<ActionResult<List<NotificationDTO>>> GetPendingByUser(int userId)
    {
        try
        {
            // Pido las notificaciones pendientes al repositorio
            var notifications = await _notificacionRepo.GetPendingByUserAsync(userId);
            return Ok(notifications);
        }
        catch
        {
            return StatusCode(500, "Error inesperado al obtener las notificaciones.");
        }
    }

    // =======================================================
    // OBTENER SOLO LA CANTIDAD DE NOTIFICACIONES PENDIENTES
    // =======================================================
    [HttpGet("count/{userId}")]
    public async Task<ActionResult<int>> GetCount(int userId)
    {
        // Uso el mismo método que arriba pero solo cuento cuántas son
        var list = await _notificacionRepo.GetPendingByUserAsync(userId);
        return Ok(list.Count);
    }

    // =======================================================
    // MARCAR UNA NOTIFICACIÓN COMO LEÍDA
    // =======================================================
    [HttpPut("{notificationId}/markasread")]
    public async Task<IActionResult> MarkAsRead(long notificationId)
    {
        try
        {
            // Busco la notificación en la base para obtener también el UserId
            var notification = await _context.Notifications.FindAsync(notificationId);

            // Si no existe, devuelvo 404
            if (notification == null)
                return NotFound($"No existe la notificación con el Id: {notificationId}.");

            // La marco como leída
            notification.IsPending = false;
            await _context.SaveChangesAsync();

            // Aviso por SignalR solo al usuario dueño de la notificación
            await _hubContext.Clients.User(notification.UserId.ToString())
                .SendAsync("NotificationUpdated");

            return NoContent();
        }
        catch
        {
            return StatusCode(500, "Error inesperado al marcar la notificación como leída.");
        }
    }

    // =======================================================
    // CREAR UNA NOTIFICACIÓN (MODO TEST)
    // =======================================================
    // Nota: este endpoint lo estoy usando solo para probar SignalR.
    [HttpPost("send-test")]
    public async Task<IActionResult> Post([FromBody] NotificationDTO dto)
    {
        // Validación mínima para evitar errores si el mensaje viene vacío
        if (string.IsNullOrWhiteSpace(dto.Message))
            return BadRequest("El mensaje no puede estar vacío.");

        // Creo la entidad con los datos del DTO
        var notificationEntity = new Notification
        {
            Message = dto.Message,
            CreatedAt = DateTime.UtcNow,   // Mantengo la misma convención que el repositorio
            IsPending = true,
            UserId = dto.UserId
        };

        await _context.Notifications.AddAsync(notificationEntity);
        await _context.SaveChangesAsync();

        // Envío la notificación en tiempo real al usuario correspondiente
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
