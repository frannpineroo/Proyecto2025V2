using System.ComponentModel.DataAnnotations;

namespace Proyecto2025.Shared.DTO
{
    public class CrearChatDTO
    {
        public string Name { get; set; } = string.Empty;
        public bool IsGroup { get; set; }
        public List<long> UserIds { get; set; } = new();
    }
}
