using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Repositorio.Repositorios
{
    public interface INotificacionRepositorio
    {
        Task<Notification> CrearNotificacionAsync(CrearNotificacionDTO dto);
        Task<List<Notification>> ObtenerNotificacionesPorUsuarioAsync(long userId);
        Task<bool> MarcarComoLeidaAsync(long id);
        Task<bool> EliminarNotificacionAsync(long id);
        
    }
}
