using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Chat
    {
        [Required(ErrorMessage = "El ID de pais es obligatorio")]
        public required long Id { get; set; }

        [Required(ErrorMessage = "El nombre del chat es obligatorio")]
        public required string? Name { get; set; }
        public bool IsGroup { get; set; } = false;
        public bool IsModerated { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "La organizacion es obligatoria")]
        public required long? OrganizationId { get; set; }
        public Organization? Organization { get; set; } = null;

        //public ICollection<ChatMember> Members { get; set; } = new List<ChatMember>();
        //public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
