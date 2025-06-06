namespace Proyecto2025.BD.Datos.Entity
{
    public class Notification
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;

        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsPending { get; set; } = true;
    }
}
