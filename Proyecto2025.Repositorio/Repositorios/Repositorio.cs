using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Repositorio.Repositorios
{
    public class Repositorio<E> : IRepositorio<E> where E : class, IEntityBase
    {
        private readonly AppDbContext context;

        public Repositorio(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<E>> SelectById()
        {
            //var chats = await context.Chats.ToListAsync();
            return await context.Set<E>().ToListAsync();
        }

        public async Task<int> Insert(E entidad)
        {
            try
            {
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> existe(int id)
        {
            bool existe = await context.Set<E>().AnyAsync(x => x.Id == id);
            return existe;
        }
        public async Task<bool> Update(int id, E entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }
            bool existeEntidad = await existe(id);
            if (!existeEntidad) return false;

            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> Delete(long id)
        {
            //var chat = await context.Chats.FindAsync(id);
            var entidad = await context.Set<E>().FirstOrDefaultAsync(e => e.Id == id);
            if (entidad == null)
            {
                return false;
            }
            try
            {
                context.Set<E>().Remove(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { throw ; }
        }

        public async Task<List<ListaChatDTO>> SelectListaChat()
        {
            var lista = await context.Chats
                .Select(p => new ListaChatDTO
            {
                Id = p.Id,
                Name = $"{p.Name} - {p.IsGroup} ({ p.IsModerated })"
                })
                .ToListAsync();

            return lista;
        }
    }
}
