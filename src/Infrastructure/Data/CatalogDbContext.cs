using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.CatalogAggregate;
using ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class CatalogDbContext:DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options):base(options) { }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
