using OneOf;
using OneOf.Types;
using ProductCatalog.Models.Dto;
using ProductCatalog.Models.Requests;

namespace ProductCatalog.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<OneOf<ProductDto, NotFound>> GetProductByIdAsync(int productId);
        Task<OneOf<int, Error>> AddProductAsync(AddProductRequest addProductRequest);
        Task<OneOf<int, NotFound>> UpdateProductAsync(int productId, UpdateProductRequest updateProductRequest);
        Task<OneOf<int, NotFound>> DeleteProductAsync(int productId);
        Task<OneOf<IEnumerable<ProductDto>, Error>> GetTopProductsAsync(int count);
    }
}
