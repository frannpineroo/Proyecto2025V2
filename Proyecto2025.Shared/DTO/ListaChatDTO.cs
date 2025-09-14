using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Shared.DTO
{
    public class ListaChatDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsGroup { get; set; } = false;
        public bool IsModerated { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public long? OrganizationId { get; set; }
    }
}
