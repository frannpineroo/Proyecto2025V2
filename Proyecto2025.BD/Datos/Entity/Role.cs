namespace Proyecto2025.BD.Datos.Entity
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
