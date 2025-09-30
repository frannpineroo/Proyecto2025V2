using Proyecto2025.Shared.ENUM;
using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Message : EntityBase
    {

        [Required(ErrorMessage = "El ID del chat es obligatorio")]
        public required long ChatId { get; set; }
        public Chat Chat { get; set; } = null!;

        [Required(ErrorMessage = "El ID del remitente es obligatorio")]
        public required long SenderId { get; set; }
        public User Sender { get; set; } = null!;

        [Required(ErrorMessage = "El contenido del mensaje es obligatorio")]
        [MaxLength(2000, ErrorMessage = "La cantidad maxima de caracteres es {2000}")]
        public string Content { get; set; } = null!;
        public MessageType MessageType { get; set; } = MessageType.text;
        public byte[]? MediaFile { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsArchived { get; set; } = false;
        public bool IsRead { get; set; } = false;
    }
}
