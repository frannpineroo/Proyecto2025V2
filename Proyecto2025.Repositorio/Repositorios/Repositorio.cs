using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
<<<<<<< HEAD
using Proyecto2025.BD.Datos.Entity;
=======
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using Proyecto2025.Shared.DTO;
=======
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8

namespace Proyecto2025.Repositorio.Repositorios
{
    public class Repositorio<E> : IRepositorio<E> where E : class, IEntityBase
<<<<<<< HEAD
    {
        private readonly AppDbContext context;

=======
    //public class Repositorio<E> where E : class, IRepositorio<E>
    {
        private readonly AppDbContext context;
        
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8
        public Repositorio(AppDbContext context)
        {
            this.context = context;
        }

<<<<<<< HEAD
        public async Task<List<E>> SelectById()
        {
            //var chats = await context.Chats.ToListAsync();
            return await context.Set<E>().ToListAsync();
        }

        public async Task<int> Insert(E entidad)
=======
        public async Task<List<E>> Select()
        {

            return await context.Set<E>().ToListAsync();
        }

        public async Task<E?> SelectById(long id)
        {
            return await context.Set<E>().FindAsync(id);
        }

        public async Task<long> Insert(E entidad)
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8
        {
            try
            {
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
<<<<<<< HEAD
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

=======
                return (long)typeof(E).GetProperty("Id")!.GetValue(entidad)!;
            }
            catch (Exception e)
            {
                // Manejo de excepciones, logueo, etc.
                throw new Exception("Error al insertar la entidad.", e);
            }
        }

        public async Task<bool> Update(long id,E entidad)
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
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8
            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
<<<<<<< HEAD
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
=======
            catch (Exception e)
            {
                // Manejo de excepciones, logueo, etc.
                throw new Exception("Error al actualizar la entidad.", e);
            }
            
        }


>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8
    }
}
