using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Organization
    {
        [Required(ErrorMessage = "El ID de organizacion es obligatorio")]
        public required long Id { get; set; }
        public required string Name { get; set; }

        [Required(ErrorMessage = "El ID del administrador es obligatorio")]
        public required long? AdminId { get; set; }
        public User? Admin { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
    }
}
