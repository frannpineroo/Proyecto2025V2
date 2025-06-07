using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Notification
    {
        [Required(ErrorMessage = "El ID de la notificación es obligatorio")]
        public required long Id { get; set; }

        [Required(ErrorMessage = "El ID de usuario es obligatorio")]
        public required long UserId { get; set; }
        public User User { get; set; } = null!;

        [Required(ErrorMessage = "El tipo de notificación es obligatorio")]
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsPending { get; set; } = true;
    }
}
