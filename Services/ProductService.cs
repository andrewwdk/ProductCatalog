using Mapster;
using Microsoft.Extensions.Caching.Memory;
using OneOf;
using OneOf.Types;
using ProductCatalog.Entities;
using ProductCatalog.Models.Dto;
using ProductCatalog.Models.Requests;
using ProductCatalog.Repository;

namespace ProductCatalog.Services
{
    public class ProductService : IProductService
    {
        private const int CacheTimeOffset = 60;
        private readonly string TopProductsCacheKey = "TopProducts";

        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;
        private readonly IMemoryCache _memoryCache;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, IMemoryCache memoryCache)
        {
            _productRepository = productRepository;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return products.Adapt<List<ProductDto>>();
        }

        public async Task<OneOf<ProductDto, NotFound>> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product.Value is NotFound)
                return product.AsT1;

            return product.AsT0.Adapt<ProductDto>();
        }

        public async Task<OneOf<int, Error>> AddProductAsync(AddProductRequest addProductRequest)
        {
            var productToAdd = Product.Create(addProductRequest.Name, addProductRequest.Description, addProductRequest.Price);

            return await _productRepository.AddProductAsync(productToAdd);
        }

        public async Task<OneOf<int, NotFound>> UpdateProductAsync(int productId, UpdateProductRequest updateProductRequest)
        {
            var productToUpdate = await _productRepository.GetProductByIdAsync(productId);

            if(productToUpdate.Value is NotFound)
                return productToUpdate.AsT1;

            productToUpdate.AsT0.Update(updateProductRequest.Name, updateProductRequest.Description, updateProductRequest.Price);

            return await _productRepository.UpdateProductAsync(productToUpdate.AsT0);
        }

        public async Task<OneOf<int, NotFound>> DeleteProductAsync(int productId)
        {
            return await _productRepository.DeleteProductAsync(productId);
        }

        public async ValueTask<OneOf<IEnumerable<ProductDto>, Error>> GetTopProductsAsync(int count)
        {
            if (count <= 0)
                return new Error();

            if (_memoryCache.TryGetValue(TopProductsCacheKey, out List<ProductDto>? cachedValue) && cachedValue != null && cachedValue.Count == count)
                return cachedValue;

            var products = (await _productRepository.GetTopProductsAsync(count)).Adapt<List<ProductDto>>();

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheTimeOffset));
            _memoryCache.Set(TopProductsCacheKey, products, cacheEntryOptions);
            return products;
        }
    }
}
