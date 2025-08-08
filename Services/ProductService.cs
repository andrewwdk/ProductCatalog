using Mapster;
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
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            _logger.LogInformation("GetProductsAsync is requested");
            var products = await _productRepository.GetProductsAsync();
            return products.Adapt<List<ProductDto>>();
        }

        public async Task<OneOf<ProductDto, NotFound>> GetProductByIdAsync(int productId)
        {
            _logger.LogInformation("GetProductByIdAsync is requested");

            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product.Value is NotFound)
                return product.AsT1;

            return product.AsT0.Adapt<ProductDto>();
        }

        public async Task<OneOf<int, Error>> AddProductAsync(AddProductRequest addProductRequest)
        {
            _logger.LogInformation("AddProductAsync is requested");

            var productToAdd = Product.Create(addProductRequest.Name, addProductRequest.Description, addProductRequest.Price);

            return await _productRepository.AddProductAsync(productToAdd);
        }

        public async Task<OneOf<int, NotFound>> UpdateProductAsync(int productId, UpdateProductRequest updateProductRequest)
        {
            _logger.LogInformation("UpdateProductAsync is requested");

            var productToUpdate = await _productRepository.GetProductByIdAsync(productId);

            if(productToUpdate.Value is NotFound)
                return productToUpdate.AsT1;

            productToUpdate.AsT0.Update(updateProductRequest.Name, updateProductRequest.Description, updateProductRequest.Price);

            return await _productRepository.UpdateProductAsync(productToUpdate.AsT0);
        }

        public async Task<OneOf<int, NotFound>> DeleteProductAsync(int productId)
        {
            _logger.LogInformation("DeleteProductAsync is requested");

            return await _productRepository.DeleteProductAsync(productId);
        }

        public async Task<OneOf<IEnumerable<ProductDto>, Error>> GetTopProductsAsync(int count)
        {
            _logger.LogInformation("GetTopProductsAsync is requested");

            if (count <= 0)
                return new Error();

            var products = await _productRepository.GetTopProductsAsync(count);
            return products.Adapt<List<ProductDto>>();
        }
    }
}
