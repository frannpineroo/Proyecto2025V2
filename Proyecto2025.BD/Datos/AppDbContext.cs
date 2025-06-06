using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos.Entity;

namespace Proyecto2025.BD.Datos
{
    public class AppDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set; }
        public DbSet<ChatStatistic> ChatStatistics { get; set; }
        public DbSet<ExternalIntegration> ExternalIntegrations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí puedes configurar tus entidades, relaciones, etc.

            // Configurar relacion explicita entre Organization y Admin (User)
            modelBuilder.Entity<Organization>()
                .HasOne(o => o.Admin)
                .WithMany() //Sin navegacion inversa
                .HasForeignKey(o => o.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            // Poniendo dos claves foraneas en UserRole
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.IdUser, ur.IdRole });
        }
    }
}
