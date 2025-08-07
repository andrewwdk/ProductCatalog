using ProductCatalog.Models.Dto;

namespace ProductCatalog.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
    }
}
