using Microsoft.EntityFrameworkCore;

namespace Projekt.Entities
{
    public class PizzeriaDbContext : DbContext
    {
        private readonly string connectionString = @"Server=DESKTOP-BHP44RA\SQLEXPRESS;Database=PizzeriaDataBase;Trusted_Connection=True;";
        public DbSet<Pizzeria> Pizzerias { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pizzeria>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Menu>().Property(m => m.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Address>().Property(a => a.City).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Address>().Property(a => a.Street).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(a => a.Email).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Role>().Property(a => a.Name).IsRequired().HasMaxLength(50);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
