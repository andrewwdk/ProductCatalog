using ProductCatalog.Entities;

namespace ProductCatalog.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
