using Microsoft.EntityFrameworkCore;
using ProductCatalog.Entities;

namespace ProductCatalog.DbContexts
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            new ProductDbInitializer(modelBuilder).Seed();
        }
    }

    public class ProductDbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public ProductDbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Product>()
                        .HasData(
                            new Product() { ProductId = 1, Name = "Table", Price = 100 },
                            new Product() { ProductId = 2, Name = "Chair", Price = 50 }
                        );
        }
    }
}
