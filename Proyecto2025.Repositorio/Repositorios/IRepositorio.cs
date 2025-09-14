using Proyecto2025.BD.Datos;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Repositorio.Repositorios
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
        Task<int> Insert(E entidad);
        Task<List<E>> SelectById();
        Task<bool> existe(int id);
        Task<bool> Update(int id, E entidad);
        Task<bool> Delete(long id);
        Task<List<ListaChatDTO>> SelectListaChat();
    }

}