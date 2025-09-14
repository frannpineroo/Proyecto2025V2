using Proyecto2025.Shared.ENUM;

namespace Proyecto2025.BD.Datos
{
    public interface IEntityBase
    {
        EstadoRegistro EstadoRegistro { get; set; }
        int Id { get; set; }
        long ChatId { get; set; }
    }
}