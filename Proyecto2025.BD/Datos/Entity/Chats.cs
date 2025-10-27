using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Chats : EntityBase
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "El nombre del chat es obligatorio")]
        [MaxLength(25, ErrorMessage = "La cantidad maxima de caracteres es {25}")]
        public required string? Name { get; set; }
        [Required(ErrorMessage ="Es un grupo es obligatorio")]
        public bool IsGroup { get; set; } = false;
        [Required(ErrorMessage = "Es moderado es obligatorio")]
        public bool IsModerated { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //public ICollection<ChatMembers> ChatMembers { get; set; } = new List<ChatMembers>();

    }
}
