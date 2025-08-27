using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Chat : EntityBase
    {

        [Required(ErrorMessage = "El nombre del chat es obligatorio")]
        [MaxLength(25,ErrorMessage = "La cantidad maxima de caracteres es {25}")]
        public required string? Name { get; set; }
        public bool IsGroup { get; set; } = false;
        public bool IsModerated { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "La organizacion es obligatoria")]
        public required long OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        //public ICollection<ChatMember> Members { get; set; } = new List<ChatMember>();
        //public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
