using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using ProductCatalog.DbContexts;
using ProductCatalog.Entities;
using ProductCatalog.Models.Dto;

namespace ProductCatalog.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            _dbContext = productDbContext;
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
                .AsNoTracking()
                .Where(p => p.ProductId == productId)
                .FirstOrDefaultAsync();

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

        public async Task<OneOf<int, NotFound>> UpdateProductAsync(int productId, UpdateProductDto updateProductDto)
        {
            var productToUpdate = await _dbContext.Products.FindAsync(productId);

            if (productToUpdate is null)
                return new NotFound();

            productToUpdate.Update(updateProductDto.Name, updateProductDto.Description, updateProductDto.Price);
            await _dbContext.SaveChangesAsync();
            return productToUpdate.ProductId;
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
    }
}
