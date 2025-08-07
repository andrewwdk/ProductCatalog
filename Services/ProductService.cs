using Mapster;
using ProductCatalog.Models.Dto;
using ProductCatalog.Repository;

namespace ProductCatalog.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return products.Adapt<List<ProductDto>>();
        }
    }
}
