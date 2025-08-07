using Microsoft.EntityFrameworkCore;
using ProductCatalog.DbContexts;
using ProductCatalog.Entities;

namespace ProductCatalog.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext productDbContext) 
        {
            _dbContext = productDbContext;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
    }
}
