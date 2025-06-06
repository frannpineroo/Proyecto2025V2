namespace Proyecto2025.BD.Datos.Entity
{
    public class ChatStatistic
    {
        public long Id { get; set; }
        public long OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;

        public string DateRange { get; set; } = null!;
        public string? Trends { get; set; }
    }
}
