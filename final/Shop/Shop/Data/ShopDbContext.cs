using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options) { }

        public DbSet<ShopItem> ShopItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactMessage> Message { get; set; }
    }
}
