namespace Proyecto2025.BD.Datos.Entity
{
    public class Message
    {
        public long Id { get; set; }

        public long ChatId { get; set; }
        public Chat Chat { get; set; } = null!;

        public long? SenderId { get; set; }
        public User? Sender { get; set; }

        public string Content { get; set; } = null!;
        public string MessageType { get; set; } = "text";
        public byte[]? MediaFile { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsArchived { get; set; } = false;
        public bool IsRead { get; set; } = false;
    }
}
