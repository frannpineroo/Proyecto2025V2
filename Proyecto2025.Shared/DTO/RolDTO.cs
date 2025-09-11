using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Shared.DTO
{
    public class RolDTO
    {
        [Required(ErrorMessage = "El ID es obligatorio")]
        public long Id { get; set; } = 0;

        [Required(ErrorMessage = "Debe ingresar un nombre de rol")]
        [MaxLength(45, ErrorMessage = "La cantidad maxima de caracteres es {45}")]
        public string? Name { get; set; }
    }
}
