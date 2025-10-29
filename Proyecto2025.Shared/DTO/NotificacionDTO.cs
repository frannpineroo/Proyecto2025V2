using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Shared.DTO
{
    public class NotificationDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsPending { get; set; }
        
    }
}
