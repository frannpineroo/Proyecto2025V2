//using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.Shared.DTO
{
    public class CrearMensajeDTO
    {
        [Required]
        public long ChatId { get; set; } //= 2;

        [Required]
        public long SenderId { get; set; } //= 3;

    [Required]
      [MaxLength(2000, ErrorMessage = "El mensaje no puede tener mas de 2000 caracteres")]
      public string Content { get; set; } = string.Empty;

    [Required]
     public string MessageType { get; set; } = "text";


     public byte[]? MediaFile { get; set; }

    [Required]
      public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
