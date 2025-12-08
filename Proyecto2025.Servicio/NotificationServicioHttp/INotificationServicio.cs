using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proyecto2025.Shared.DTO;

public interface INotificationServicio
{
    Task<List<NotificationDTO>> ObtenerPendientes(int userId);
    Task<int> ObtenerCantidad(int userId);
    Task Crear(NotificationDTO dto);
    Task MarcarLeida(long id);
}

