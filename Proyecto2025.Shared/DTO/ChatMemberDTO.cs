using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Shared.DTO
{
    public class ChatMemberDTO
    {
        
        public long Id { get; set; }

        [Required(ErrorMessage = "El ID de chat es obligatorio")]
        public long ChatId { get; set; }

        [Required(ErrorMessage = "El ID de usuario es obligatorio")]
        public long UserId { get; set; }

        public bool IsModerator { get; set; } = false;
        public bool CanWrite { get; set; } = true;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
