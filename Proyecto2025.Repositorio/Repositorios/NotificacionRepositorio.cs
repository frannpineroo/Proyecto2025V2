using System;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Repositorio.Repositorios
{
    public class NotificationRepositorio : INotificationRepositorio
    {
        private readonly AppDbContext context;

        public NotificationRepositorio(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Notification> CrearNotificationAsync(NotificationDTO dto)
        {
            var notificacion = new Notification
            {
                Message = dto.Message,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                IsPending = true
            };

            context.Notifications.Add(notificacion);
            await context.SaveChangesAsync();
            return notificacion;
        }


        public async Task<List<NotificationDTO>> GetPendingByUserAsync(int userId)
        {
            var notifications = await context.Notifications
                .Where(n => n.UserId == userId && n.IsPending)
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new NotificationDTO
                {
                    Id = n.Id,
                    Message = n.Message,
                    CreatedAt = n.CreatedAt,
                    IsPending = n.IsPending,
                    UserId = n.UserId
                })
                .ToListAsync();

            return notifications;
        }

        public async Task<bool> MarkAsReadAsync(long notificationId)
        {

            var notification = await context.Notifications.FindAsync(notificationId);
            if (notification == null) return false;
            notification.IsPending = false;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
