using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.Shared.DTO
{
    public class CrearUsuarioDTO
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
