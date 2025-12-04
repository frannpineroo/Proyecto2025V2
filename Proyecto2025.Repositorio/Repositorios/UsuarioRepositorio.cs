using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Repositorio.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext context;

        public UsuarioRepositorio(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<User> CrearUsuarioAsync(CrearUsuarioDTO dto)
        {
            var usuario = new User
            {
                Id = 0, // EF Core asignará el Id automáticamente
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                IsOnline = false,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                RoleId = dto.RoleId
            };

            context.Users.Add(usuario);
            await context.SaveChangesAsync();

            return usuario;
        }

        public async Task<User?> ObtenerUsuarioPorIdAsync(long id)
        {
            return await context.Users.Include(u => u.Role) // Include para traer el rol asociado
                                      .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> ObtenerUsuarioPorEmailAsync(string email)
        {
            return await context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> ObtenerTodosLosUsuariosAsync()
        {
            return await context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<bool> ActualizarUsuarioAsync(long id, User user)
        {
            var usuarioExistente = await context.Users.FindAsync(id);
            if (usuarioExistente == null)
            {
                return false;
            }

            // Actualizar los campos necesarios
            usuarioExistente.FirstName = user.FirstName;
            usuarioExistente.LastName = user.LastName;
            usuarioExistente.Email = user.Email;
            usuarioExistente.RoleId = user.RoleId;
            usuarioExistente.IsActive = user.IsActive;

            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar excepciones de actualización, como violaciones de clave única
                return false;
            }
        }

        public async Task<bool> EliminarUsuarioAsync(long id)
        {
            var usuario = await context.Users.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            context.Users.Remove(usuario);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteUsuarioAsync(long id)
        {
            return await context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<List<User>> SelectUsuarios(string filtro)
        {
            return await context.Users
                .Where(u => u.FirstName.ToLower().Contains(filtro)
                    || u.LastName.ToLower().Contains(filtro)
                    || u.Email.ToLower().Contains(filtro))
                .ToListAsync();
        }
        public async Task<List<User>> GetActivos()
        {
            return await context.Users
                .Where(u => u.IsActive)
                .ToListAsync();
        }
        public async Task<List<User>> GetInactivos()
        {
            return await context.Users
                .Where(u => !u.IsActive)
                .ToListAsync();
        }

        public async Task<bool> DesactivarUsuarios(long id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return false;

            user.IsActive = false;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivarUsuarios(long id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return false;

            user.IsActive = true;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
