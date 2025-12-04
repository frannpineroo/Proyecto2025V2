using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Proyecto2025.Shared.DTO
{
    public class ListaUsuarioDTO
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsOnline { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public required long RoleId { get; set; }
    }
}
