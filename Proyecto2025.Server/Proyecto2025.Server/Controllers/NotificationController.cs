using Microsoft.AspNetCore.Mvc;
using Proyecto2025.Repositorio.Repositorios; 
using Proyecto2025.Shared.DTO;
using System;

[ApiController]
[Route("api/Notification")] 
public class NotificationsController : ControllerBase 
{
    
    private readonly INotificacionRepositorio _notificacionRepo;

  
    public NotificationsController(INotificacionRepositorio notificacionRepo)
    {
        _notificacionRepo = notificacionRepo;
    }


    [HttpGet("user/{userId}/pending")]
    public async Task<ActionResult<List<NotificationDto>>> GetPendingByUser(int userId)
    {
        try
        {
            var notifications = await _notificacionRepo.GetPendingByUserAsync(userId);
            // Caso de éxito
            return Ok(notifications);
        }
        catch (Exception ex)
        {
            // Caso de error inesperado (ej. la BD se cayó)
            // En un proyecto real, aquí guardarías el error 'ex.Message' en un log
            return StatusCode(500, "Error inesperado al obtener las notificaciones.");
        }
    }

    [HttpPost("{notificationId}/markasread")]
    public async Task<IActionResult> MarkAsRead(long notificationId)
    {
        try
        {
            var exito = await _notificacionRepo.MarkAsReadAsync(notificationId);

            if (!exito)
            {
                // Caso de "error esperado" (no se encontró)
                return NotFound($"No existe la notificación con el Id: {notificationId}.");
            }

            // Caso de éxito
            return NoContent();
        }
        catch (Exception ex)
        {
            // Caso de error inesperado
            return StatusCode(500, "Error inesperado al marcar la notificación como leída.");
        }
    }
}