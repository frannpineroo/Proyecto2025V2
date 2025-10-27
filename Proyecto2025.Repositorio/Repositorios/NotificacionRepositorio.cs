using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Repositorio.Repositorios
{
    public class NotificacionRepositorio : INotificacionRepositorio
    {
        private readonly AppDbContext context;

        public NotificacionRepositorio(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Notification> CrearNotificacionAsync(CrearNotificacionDTO dto)
        {
            var notificacion = new Notification
            {
                Id = default,
                Message = dto.Message,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                IsPending = true
            };

            context.Notifications.Add(notificacion);
            await context.SaveChangesAsync();

            return notificacion;
        }

        public async Task<List<Notification>> ObtenerNotificacionesPorUsuarioAsync(long userId)
        {
            return await context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> MarcarComoLeidaAsync(long id)
        {
            var notif = await context.Notifications.FindAsync(id);
            if (notif == null) return false;

            notif.IsPending = true;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarNotificacionAsync(long id)
        {
            var notif = await context.Notifications.FindAsync(id);
            if (notif == null) return false;

            context.Notifications.Remove(notif);
            await context.SaveChangesAsync();
            return true;
        }
    }
}