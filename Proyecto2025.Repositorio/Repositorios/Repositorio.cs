using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2025.Repositorio.Repositorios
{
    public class Repositorio<E> : IRepositorio<E> where E : class, IEntityBase
    //public class Repositorio<E> where E : class, IRepositorio<E>
    {
        private readonly AppDbContext context;
        
        public Repositorio(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<E>> Select()
        {

            return await context.Set<E>().ToListAsync();
        }

        public async Task<bool> Existe(long id)
        {
            bool existe = await context.Set<E>()
                                       .AnyAsync(x => x.Id == id);
            return existe;
        }
        public async Task<E?> SelectById(long id)
        {
            return await context.Set<E>().FindAsync(id);
        }

        public async Task<long> Insert(E entidad)
        {
            try
            {
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return (entidad.Id);
            }
            catch (Exception e)
            {
                // Manejo de excepciones, logueo, etc.
                throw new Exception("Error al insertar la entidad.", e);
            }
        }
  
        public async Task<bool> Update(long id,E entidad)
        {
            if (id != entidad.Id)
            {
                throw new ArgumentException("El ID de la entidad no coincide con el ID proporcionado.");
            }
            var existe = await Existe(id);
            //var existe = await context.Set<E>().FindAsync(id);
            if (!existe)
            {
                throw new KeyNotFoundException("La entidad no existe.");
            }
            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                // Manejo de excepciones, logueo, etc.
                throw new Exception("Error al actualizar la entidad.", e);
            }
            
        }



    }
}
