using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Shared.DTO;

public interface INotificationRepositorio
{

    Task<Notification> CrearNotificationAsync(NotificationDTO dto);

    Task<List<NotificationDTO>> GetPendingByUserAsync(int userId);
    Task<bool> MarkAsReadAsync(long notificationId);
}
