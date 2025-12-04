using System.Reflection.Metadata;
using System.Threading.Tasks.Dataflow;
using System;

namespace Proyecto2025.Shared.DTO
{
    public class VerMensajesDTO
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string ChatName { get; set; } = string.Empty;
        public int SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int MessageType { get; set; }
        public string? MediaFile { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public bool IsArchived { get; set; }
    }
}
