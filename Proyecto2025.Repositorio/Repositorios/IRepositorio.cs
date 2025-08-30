
using Proyecto2025.BD.Datos;

namespace Proyecto2025.Repositorio.Repositorios
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
        Task<long> Insert(E entidad);
        Task<List<E>> Select();
        Task<E?> SelectById(long id);
        //Task Update(E entidad);
        Task<bool> Update(long id, E entidad);
    }
}