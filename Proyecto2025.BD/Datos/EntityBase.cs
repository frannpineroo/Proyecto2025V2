using Proyecto2025.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.BD.Datos
{
    public class EntityBase : IEntityBase
    {
<<<<<<< HEAD
        public EstadoRegistro EstadoRegistro { get; set; }
        public int Id { get; set; }

        public long ChatId { get; set; }
=======
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto incremento
        [Required(ErrorMessage = "El ID es obligatorio")]
        public required long Id { get; set; } 
        //public EstadoRegistro EstadoRegistro { get; set; }
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8
    }
}
