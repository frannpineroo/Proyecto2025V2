using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.Shared.DTO
{
    public class ChatsDTO
    {
        //[Required(ErrorMessage = "El ID de chat es obligatorio")]
        public long Id { get; set; }

        [Required(ErrorMessage = "El nombre del chat es obligatorio")]
        [MaxLength(25, ErrorMessage = "La cantidad maxima de caracteres es {25}")]
        public string? Name { get; set; }
        public bool IsGroup { get; set; } = false;

        //[Required(ErrorMessage = "Es un grupo es obligatorio")]
        public bool IsModerated { get; set; } = false;

        //[Required(ErrorMessage = "Es moderador es obligatorio")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        //[Required(ErrorMessage = "La organizacion es obligatoria")]
        //public long? OrganizationId { get; set; }
    }

}
