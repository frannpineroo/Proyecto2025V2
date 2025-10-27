using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Shared.DTO
{
    public class ChatMembersDTO
    {
        //[Required(ErrorMessage = "El ID de miembro es obligatorio")]
        public long Id { get; set; }

        [Required(ErrorMessage = "El ID de chat es obligatorio")]
        public long ChatId { get; set; }

        [Required(ErrorMessage = "El ID de usuario es obligatorio")]
        public long UserId { get; set; }

        //[Required(ErrorMessage = "si es moderador es obligatorio")]
        public bool IsModerator { get; set; } = false;
        //[Required(ErrorMessage = "si puede escribir es obligatorio")]
        public bool CanWrite { get; set; } = true;
        //[Required(ErrorMessage = "cuando se unio es obligatorio")]
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
