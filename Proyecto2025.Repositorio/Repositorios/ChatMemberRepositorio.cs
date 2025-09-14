using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Repositorio.Repositorios
{
    public class ChatMemberRepositorio<E> : IChatMemberRepositorio<E> where E : class, IEntityBase
    {
        private readonly AppDbContext context;
        public ChatMemberRepositorio(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<List<E>> SelectByChatId(long chatId)
        {
            //var members = await context.ChatMembers
            return await context.Set<E>().Where(cm => cm.ChatId == chatId).ToListAsync();
        }

        public async Task<int> Insert(E miembrochat)
        {
            try
            {
                await context.Set<E>().AddAsync(miembrochat);
                await context.SaveChangesAsync();
                return miembrochat.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(int id, E miembrochat)
        {
             if (id != miembrochat.Id)
            {
                return false;
            }

            bool existeEntidad = await context.Set<E>().AnyAsync(x => x.Id == id);
            if (!existeEntidad) return false;
            try
            {
                context.Set<E>().Update(miembrochat);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> Delete(int id)
        {
            //var member = await context.ChatMembers.FindAsync(id);
            var entity = await context.Set<E>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            context.Set<E>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }

       
    }
}
