using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class UserRole
    {
        public long IdUser { get; set; }
        public User User { get; set; } = null!;

        public long IdRole { get; set; }
        public Role Role { get; set; } = null!;
    }
}
