using Proyecto2025.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.BD.Datos
{
    public class EntityBase : IEntityBase
    {
        public EstadoRegistro EstadoRegistro { get; set; }
        public int Id { get; set; }

        public long ChatId { get; set; }
    }
}
