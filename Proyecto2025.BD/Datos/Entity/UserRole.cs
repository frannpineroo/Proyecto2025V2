using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class UserRole
    {
        [Required(ErrorMessage = "El ID de usuario es obligatorio")]
        public required long IdUser { get; set; }
        public User User { get; set; } = null!;

        [Required(ErrorMessage = "El ID de rol es obligatorio")]
        public required long IdRole { get; set; }
        public Role Role { get; set; } = null!;
    }
}
