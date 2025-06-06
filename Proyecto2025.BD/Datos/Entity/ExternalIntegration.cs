namespace Proyecto2025.BD.Datos.Entity
{
    public class ExternalIntegration
    {
        public long Id { get; set; }
        public long DeveloperId { get; set; }
        public User Developer { get; set; } = null!;

        public string ApiToken { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
