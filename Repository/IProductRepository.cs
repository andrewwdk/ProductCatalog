using OneOf;
using OneOf.Types;
using ProductCatalog.Entities;
using ProductCatalog.Models.Dto;

namespace ProductCatalog.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<OneOf<Product, NotFound>> GetProductByIdAsync(int productId);
        Task<OneOf<int, Error>> AddProductAsync(Product product);
        Task<OneOf<int, NotFound>> UpdateProductAsync(int productId, UpdateProductDto updateProductDto);
        Task<OneOf<int, NotFound>> DeleteProductAsync(int productId);
    }
}
