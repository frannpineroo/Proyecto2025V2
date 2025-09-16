using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;

namespace Proyecto2025.Repositorio.Repositorios
{
    public class MensajeRepositorio : IMensajeRepositorio
    {
        private readonly AppDbContext context;

        public MensajeRepositorio(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Message> CreateAsync(Message mensaje)
        {
            context.Messages.Add(mensaje);
            await context.SaveChangesAsync();

            // Cargar datos relacionados para el DTO
            return await context.Messages
                .Include(m => m.Chat)
                .Include(m => m.Sender)
                .Include(m => m.Sender)
                    .ThenInclude(s => s.Role)
                .FirstAsync(m => m.Id == mensaje.Id);
        }

        public async Task<Message?> GetByIdAsync(int id)
        {
            return await context.Messages
                .Include(m => m.Chat)
                .Include(m => m.Sender)
                .Include(m => m.Sender)
                    .ThenInclude(s => s.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Message>> GetByChatIdAsync(int chatId)
        {
            return await context.Messages
                .Include(m => m.Chat)
                .Include(m => m.Sender)
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task<Message> UpdateAsync(Message message)
        {
            context.Messages.Update(message);
            await context.SaveChangesAsync();
            return message;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var message = await context.Messages.FindAsync(id);
            if (message == null) return false;

            context.Messages.Remove(message);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
