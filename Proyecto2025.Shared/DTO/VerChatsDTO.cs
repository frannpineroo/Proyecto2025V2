namespace Proyecto2025.Shared.DTO
{
    public class VerChatsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsGroup { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public int UnreadMessageCount { get; set; } // Calculado
        public DateTime? LastMessageAt { get; set; } // Calculado
    }
}
