using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }

        // جداول اصلی پروژه
        public DbSet<User> Users { get; set; }
        public DbSet<ShopItem> ShopItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactMessage> Message { get; set; }

        // جداول Role / Permission
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تعریف کلید اصلی ترکیبی برای جدول RolePermission
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.FkRole, rp.FkPermission });

            // تعریف رابطه RolePermission -> Role
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.FkRole)
                .OnDelete(DeleteBehavior.Cascade);

            // تعریف رابطه RolePermission -> Permission
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.FkPermission)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

