using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Servicio
{
    // Esta interfaz define qué operaciones debe implementar el servicio de notificaciones.
    // La uso para poder inyectarlo en Blazor y mantener la lógica separada.
    public interface INotificationServicio
    {
        // Devuelve las notificaciones que el usuario todavía no leyó.
        Task<List<NotificationDTO>> ObtenerPendientes(int userId);

        // Devuelve la cantidad de notificaciones pendientes del usuario.
        Task<int> ObtenerCantidad(int userId);

        // Crea una nueva notificación en el sistema.
        Task Crear(NotificationDTO dto);

        // Marca una notificación como leída usando su ID.
        Task MarcarLeida(long id);
    }
}
