using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Organization : EntityBase
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de organización")]
        [MaxLength(45, ErrorMessage = "La cantidad maxima de caracteres es {45}")]
        public required string Name { get; set; }

        //[Required(ErrorMessage = "El ID del administrador es obligatorio")]
        //public required long AdminId { get; set; }
        //public User? Admin { get; set; }

        //public ICollection<User> Users { get; set; } = new List<User>();
        //public ICollection<Chat> Chats { get; set; } = new List<Chat>();
    }
}
