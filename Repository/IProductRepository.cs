using OneOf;
using OneOf.Types;
using ProductCatalog.Entities;

namespace ProductCatalog.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<OneOf<Product, NotFound>> GetProductByIdAsync(int productId);
        Task<OneOf<int, Error>> AddProductAsync(Product product);
        Task<OneOf<int, NotFound>> UpdateProductAsync(Product product);
        Task<OneOf<int, NotFound>> DeleteProductAsync(int productId);
        Task<IEnumerable<Product>> GetTopProductsAsync(int count);
    }
}
