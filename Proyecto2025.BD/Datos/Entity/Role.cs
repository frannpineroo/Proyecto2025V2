using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Role
    {
        [Required(ErrorMessage = "El ID del rol es obligatorio")]
        public required long Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
