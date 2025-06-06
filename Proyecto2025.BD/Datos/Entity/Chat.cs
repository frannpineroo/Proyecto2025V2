namespace Proyecto2025.BD.Datos.Entity
{
    public class Chat
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsGroup { get; set; } = false;
        public bool IsModerated { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public long? OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public ICollection<ChatMember> Members { get; set; } = new List<ChatMember>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
