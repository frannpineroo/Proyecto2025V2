using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Shared.DTO
{
    public class ChatDTO
    {
        [Required(ErrorMessage = "El ID de chat es obligatorio")]
        public long Id { get; set; } 

        [Required(ErrorMessage = "El nombre del chat es obligatorio")]
        public string? Name { get; set; }
        public bool IsGroup { get; set; } = false;
        public bool IsModerated { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? UltimoMensaje { get; set; }
    }

}

