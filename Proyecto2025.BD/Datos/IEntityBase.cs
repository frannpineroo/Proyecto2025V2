using Proyecto2025.Shared.ENUM;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proyecto2025.BD.Datos
{
    public interface IEntityBase
    {
        EstadoRegistro EstadoRegistro { get; set; }
        int Id { get; set; }
        long ChatId { get; set; }
    }
}
