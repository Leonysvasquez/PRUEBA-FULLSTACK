using Microsoft.EntityFrameworkCore;
using ProductSales.Models;

namespace ProductSales.Data
{
     public  class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SalesItem> SaleItems => Set<SalesItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SalesItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId);

            modelBuilder.Entity<SalesItem>()
                .HasOne(si => si.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId);
        }
    }
}
