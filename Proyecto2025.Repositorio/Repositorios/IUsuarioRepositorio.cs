using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Shared.DTO;

namespace Proyecto2025.Repositorio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<User> CrearUsuarioAsync(CrearUsuarioDTO dto);
        Task<User?> ObtenerUsuarioPorIdAsync(long id);
        Task<User?> ObtenerUsuarioPorEmailAsync(string email);
        Task<List<User>> ObtenerTodosLosUsuariosAsync();
        Task<bool> ActualizarUsuarioAsync(long id, User user);
        Task<bool> EliminarUsuarioAsync(long id);
        Task<bool> ExisteUsuarioAsync(long id);
        Task<bool> ExisteEmailAsync(string email);
        Task<List<User>> SelectUsuarios(string filtro);
        Task<List<User>> GetActivos();
        Task<List<User>> GetInactivos();
        Task<bool> DesactivarUsuarios(long id);
        Task<bool> ActivarUsuarios(long id);
    }
}