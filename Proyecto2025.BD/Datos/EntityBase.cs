using Proyecto2025.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.BD.Datos
{
    public class EntityBase : IEntityBase
    {
        public EstadoRegistro EstadoRegistro { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto incremento
        [Required(ErrorMessage = "El ID es obligatorio")]
        public long Id { get; set; }

        public long ChatId { get; set; }
    }
}
