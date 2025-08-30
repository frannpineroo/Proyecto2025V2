using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.Shared.DTO
{
    public class CrearOrganizacionDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
