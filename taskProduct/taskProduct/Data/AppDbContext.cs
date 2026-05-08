using Microsoft.EntityFrameworkCore;
using taskProduct.Models;

namespace taskProduct.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)      : base(options) { }

        public DbSet<Categories> Categories => Set<Categories>();
        public DbSet<Product> products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define One-to-Many relationship
            modelBuilder.Entity<Categories>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
