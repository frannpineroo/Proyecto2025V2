using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Shared.DTO
{
    public class VerNotificacionesDTO
    {
        public long Id { get; set; }              // Id de la notificación
        public string Message { get; set; } = ""; // Mensaje de la notificación
        public long UserId { get; set; }          // Id del usuario destinatario
        public DateTime CreatedAt { get; set; }   // Fecha de creación
        public bool IsPending { get; set; }       // Estado (pendiente/leída)
    }
}
