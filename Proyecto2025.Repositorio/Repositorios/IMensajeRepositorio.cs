using Proyecto2025.BD.Datos.Entity;

namespace Proyecto2025.Repositorio.Repositorios
{
    public interface IMensajeRepositorio
    {
        Task<Message> CreateAsync(Message mensaje);
        Task<bool> DeleteAsync(int id);
        Task<List<Message>> GetByChatIdAsync(int chatId);
        Task<Message?> GetByIdAsync(int id);
        Task<Message> UpdateAsync(Message message);
    }
}