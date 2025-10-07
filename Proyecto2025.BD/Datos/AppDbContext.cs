using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos.Entity;

namespace Proyecto2025.BD.Datos
{
    public class AppDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí puedes configurar tus entidades, relaciones, etc
}
    }
}
