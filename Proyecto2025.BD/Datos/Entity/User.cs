using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    [Index(nameof(Email), Name = "Email_UQ", IsUnique = true)]
    public class User : EntityBase
    {

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(45, ErrorMessage = "La cantidad maxima de caracteres es {45}")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(45, ErrorMessage = "La cantidad maxima de caracteres es {45}")]
        public required string LastName { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(80, ErrorMessage = "La cantidad maxima de caracteres es {80}")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(45, ErrorMessage = "La cantidad maxima de caracteres es {45}")]
        public required string Password { get; set; }

        public bool IsOnline { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "El id de rol es obligatorio")]
        public required long RoleId { get; set; }
        public Role? Role { get; set; }

    }
}
