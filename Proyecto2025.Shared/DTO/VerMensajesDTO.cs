namespace Proyecto2025.Shared.DTO
{
    public class VerMensajesDTO
    {
       public int Id { get; set; }
       public int ChatId { get; set; }
       public string ChatName { get; set; } = string.Empty; // Solo el nombre del chat
       public int SenderId { get; set; }
       public string SenderName { get; set; } = string.Empty;// Nombre completo del remitente
       public string Content { get; set; } = string.Empty;
       public int MessageType { get; set; }
       public string? MediaFile { get; set; }
       public DateTime SentAt { get; set; }
       public bool IsRead { get; set; }
    }
}
