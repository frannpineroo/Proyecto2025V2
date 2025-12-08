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
            // Guardo el contexto para poder trabajar con la base
            this.context = context;
        }

        public async Task<Notification> CrearNotificationAsync(NotificationDTO dto)
        {
            // Creo una entidad nueva a partir de los datos que vienen del DTO
            var notificacion = new Notification
            {
                Message = dto.Message,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow, // Fecha de creación en formato UTC
                IsPending = true             // Todas las notificaciones arrancan como pendientes
            };

            context.Notifications.Add(notificacion);
            await context.SaveChangesAsync(); // Guardo en la base
            return notificacion;
        }

        public async Task<List<NotificationDTO>> GetPendingByUserAsync(int userId)
        {
            // Busco las notificaciones pendientes del usuario,
            // ordenadas de más nueva a más vieja
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
            // Busco la notificación por ID
            var notification = await context.Notifications.FindAsync(notificationId);

            // Si no existe, aviso que no se pudo marcar como leída
            if (notification == null) return false;

            notification.IsPending = false; // La marco como leída
            await context.SaveChangesAsync(); // Actualizo en la base

            return true;
        }
    }
}
