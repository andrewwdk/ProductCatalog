using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using ProductCatalog.DbContexts;
using ProductCatalog.Entities;
using System.Data;

namespace ProductCatalog.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public ProductRepository(ProductDbContext productDbContext, IConfiguration configuration)
        {
            _dbContext = productDbContext;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<OneOf<Product, NotFound>> GetProductByIdAsync(int productId)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                return new NotFound();

            return product;
        }

        public async Task<OneOf<int, Error>> AddProductAsync(Product product)
        {
            var addedProduct = await _dbContext.Products.AddAsync(product);

            if (addedProduct == null)
                return new Error();

            await _dbContext.SaveChangesAsync();
            return addedProduct.Entity.ProductId;
        }

        public async Task<OneOf<int, NotFound>> UpdateProductAsync(Product product)
        {
            await _dbContext.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task<OneOf<int, NotFound>> DeleteProductAsync(int productId)
        {
            var productToDelete = await _dbContext.Products.FindAsync(productId);

            if (productToDelete is null)
                return new NotFound();

            _dbContext.Remove(productToDelete);
            await _dbContext.SaveChangesAsync();
            return productToDelete.ProductId;
        }

        public async Task<IEnumerable<Product>> GetTopProductsAsync(int count)
        {
            var procedureName = "GetTopNProducts";
            var parameters = new DynamicParameters();
            parameters.Add("Count", count, DbType.Int32, ParameterDirection.Input);

            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("ProductDb")))
            {
                var products = await connection.QueryAsync<Product>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return products;
            }
        }
    }
}
