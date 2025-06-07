using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class ChatMember
    {
        [Required(ErrorMessage = "El ID de miembro es obligatorio")]
        public required long Id { get; set; }

        [Required(ErrorMessage = "El ID de chat es obligatorio")]
        public required long ChatId { get; set; }
        public Chat Chat { get; set; } = null!;

        [Required(ErrorMessage = "El ID de usuario es obligatorio")]
        public required long UserId { get; set; }
        public User User { get; set; } = null!;

        public bool IsModerator { get; set; } = false;
        public bool CanWrite { get; set; } = true;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
