using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class ExternalIntegration
    {
        [Required(ErrorMessage = "El ID de api es obligatorio")]
        public required long Id { get; set; }

        [Required(ErrorMessage = "El ID de del desarrollador es obligatorio")]
        public required long DeveloperId { get; set; }
        public User Developer { get; set; } = null!;

        [Required(ErrorMessage = "El nombre de la integracion es obligatorio")]
        public required string ApiToken { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
