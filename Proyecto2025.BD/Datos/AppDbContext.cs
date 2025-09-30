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
<<<<<<< HEAD
        //public DbSet<Organization> Organizations { get; set; }
=======
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí puedes configurar tus entidades, relaciones, etc.

            // Configurar relacion explicita entre Organization y Admin (User)
<<<<<<< HEAD
            //modelBuilder.Entity<Organization>()
                //.HasOne(o => o.Admin)
                //.WithMany() //Sin navegacion inversa
                //.HasForeignKey(o => o.AdminId)
                //.OnDelete(DeleteBehavior.Restrict);
=======
            // modelBuilder.Entity<Organization>()
            // .HasOne(o => o.Admin)
            // .WithMany() //Sin navegacion inversa
            // .HasForeignKey(o => o.AdminId)
            // .OnDelete(DeleteBehavior.Restrict);
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8

            // Poniendo dos claves foraneas en UserRole
            // modelBuilder.Entity<UserRole>()
            //   .HasKey(ur => new { ur.IdUser, ur.IdRole });
            modelBuilder.Entity<User>()
                        .HasOne(u => u.Role)
                        .WithMany()
                        .HasForeignKey(u => u.RoleId);
        }
    }
}
