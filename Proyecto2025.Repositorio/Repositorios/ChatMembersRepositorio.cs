using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Repositorio.Repositorios
{
    public class ChatMembersRepositorio<E> : IChatMembersRepositorio<E> where E : class, IEntityBase
    {
        private readonly AppDbContext context;
        public ChatMembersRepositorio(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<List<E>> SelectByChatId(long chatId)
        {
            //var members = await context.ChatMembers
            return await context.Set<E>().Where(cm => cm.Id == chatId).ToListAsync();
        }


        public async Task<long> Insert(E miembrochat)
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
        }     //problemas solucionados solo falta probar bien con las pages los datos en el front
              //y q agarren bien los datos de cada entidad de chat y chatmembers 
              //funciona los metodos crear pero falta verificar bien los datos q toma las pages para q puedan traer 
              //y enviar bien los datos a la bd

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
