
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Shared.DTO; 

public interface INotificacionRepositorio
{
  
    Task<Notification> CrearNotificacionAsync(NotificationDto dto);

    Task<List<NotificationDto>> GetPendingByUserAsync(int userId);
    Task<bool> MarkAsReadAsync(long notificationId);
}