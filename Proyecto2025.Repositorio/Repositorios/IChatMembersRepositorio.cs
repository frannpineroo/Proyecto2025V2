using Proyecto2025.BD.Datos;

namespace Proyecto2025.Repositorio.Repositorios
{
    public interface IChatMembersRepositorio<E> where E : class, IEntityBase
    {
        Task<List<E>> SelectByChatId(long chatId);
        Task<long> Insert(E miembrochat);
        Task<bool> Update(int id, E miembrochat);
        Task<bool> Delete(int id);

    }
}