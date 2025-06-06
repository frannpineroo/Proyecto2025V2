namespace Proyecto2025.BD.Datos.Entity
{
    public class ChatMember
    {
        public long Id { get; set; }

        public long ChatId { get; set; }
        public Chat Chat { get; set; } = null!;

        public long UserId { get; set; }
        public User User { get; set; } = null!;

        public bool IsModerator { get; set; } = false;
        public bool CanWrite { get; set; } = true;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
