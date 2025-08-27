using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class Role : EntityBase
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de rol")]
        [MaxLength(45, ErrorMessage = "La cantidad maxima de caracteres es {45}")]
        public string Name { get; set; } = null!;

        //public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
