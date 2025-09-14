using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Shared.DTO
{
    public class ListaChatMemberDTO
    {
        public long Id { get; set; }
        public long ChatId { get; set; }
        public long UserId { get; set; }
        public bool IsModerator { get; set; } = false;
        public bool CanWrite { get; set; } = true;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
