using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Message
    {
        [Required(ErrorMessage = "El ID del mensaje es obligatorio")]
        public required long Id { get; set; }

        [Required(ErrorMessage = "El ID del chat es obligatorio")]
        public required long ChatId { get; set; }
        public Chat Chat { get; set; } = null!;

        [Required(ErrorMessage = "El ID del remitente es obligatorio")]
        public required long? SenderId { get; set; }
        public User? Sender { get; set; }

        [Required(ErrorMessage = "El contenido del mensaje es obligatorio")]
        public string Content { get; set; } = null!;
        public string MessageType { get; set; } = "text";
        public byte[]? MediaFile { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsArchived { get; set; } = false;
        public bool IsRead { get; set; } = false;
    }
}
