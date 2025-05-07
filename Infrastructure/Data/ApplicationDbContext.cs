
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext
            (DbContextOptions<ApplicationDbContext> options) :
            base(options)
        { }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<InventoryLog> InventoryLogs => Set<InventoryLog>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<InventoryLog>()
                .HasOne(l => l.Product)
                .WithMany()
                .HasForeignKey(l => l.ProductId);

            modelBuilder.Entity<InventoryLog>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId);

            // Seed initial data
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Full access" },
                new Role { Id = 2, Name = "Manager", Description = "Limited admin access" },
                new Role { Id = 3, Name = "Staff", Description = "Basic access" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Laptops, Mobiles, etc." },
                new Category { Id = 2, Name = "Clothing", Description = "T-Shirts, Jeans, etc." }
            );
        }
    }
}
