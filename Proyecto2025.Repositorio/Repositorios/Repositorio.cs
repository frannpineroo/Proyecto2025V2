using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.Shared.DTO;
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

        public async Task<E?> SelectById(long id)
        {
            return await context.Set<E>().FindAsync(id);
        }

        public async Task<long> Insert(E entidad)
        {
            try
            {
                var existechat = await context.Chats.AnyAsync(c => c.Id == entidad.Id); 
                if(existechat)
                  
                {
                    throw new Exception("ya existe el chat con ese nombre");
                }
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return (long)typeof(E).GetProperty("Id")!.GetValue(entidad)!;
            }
            catch (Exception e)
            {
                // Manejo de excepciones, logueo, etc.
                throw new Exception("Error al insertar la entidad.", e);
            }
        }

        public async Task<bool> Update(long id, E entidad)
        {
            if (id != (long)typeof(E).GetProperty("Id")!.GetValue(entidad)!)
            {
                throw new ArgumentException("El ID de la entidad no coincide con el ID proporcionado.");
            }
            var existe = await context.Set<E>().FindAsync(id);
            if (existe == null)
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
