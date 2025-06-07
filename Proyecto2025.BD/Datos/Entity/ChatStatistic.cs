using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.BD.Datos.Entity
{
    public class ChatStatistic
    {
        [Required(ErrorMessage = "El ID de la estadistica es obligatorio")]
        public required long Id { get; set; }

        [Required(ErrorMessage = "La organizacion es obligatoria")]
        public required long OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;

        public string DateRange { get; set; } = null!;
        public string? Trends { get; set; }
    }
}
