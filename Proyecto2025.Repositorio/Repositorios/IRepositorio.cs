
namespace Proyecto2025.Repositorio.Repositorios
{
    public interface IRepositorio<E> where E : class
    {
        Task<long> Insert(E entidad);
        Task<List<E>> Select();
        Task<E?> SelectById(long id);
        Task Update(E entidad);
        Task Update(long id, object entidad);
    }
}