using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models.Requests;
using ProductCatalog.Models.Responses;
using ProductCatalog.Services;

namespace ProductCatalog.Controllers
{
    [Route("~/api/products")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetProductsAsync();

            return Ok(result.Adapt<ProductsResponse>());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetWeightById([FromRoute] int productId)
        {
            var result = await _productService.GetProductByIdAsync(productId);

            return result.Match<IActionResult>(
                product => Ok(product.Adapt<ProductResponse>()),
                _ => NotFound());
        }

        [HttpPost()]
        public async Task<IActionResult> AddProduct([FromForm] AddProductRequest addProductRequest)
        {
            var result = await _productService.AddProductAsync(addProductRequest);

            return result.Match<IActionResult>(
                productId => Ok(new { productId }),
                _ => BadRequest());
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromBody] UpdateProductRequest updateProductRequest)
        {
            var result = await _productService.UpdateProductAsync(productId, updateProductRequest);

            return result.Match<IActionResult>(
                productId => Ok(new { productId }),
                _ => NotFound());
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            var result = await _productService.DeleteProductAsync(productId);

            return result.Match<IActionResult>(
                productId => Ok(new { productId }),
                _ => NotFound());
        }

        [HttpGet("top/{count}")]
        public async Task<IActionResult> GetTopProducts([FromRoute] int count)
        {
            var result = await _productService.GetTopProductsAsync(count);

            return result.Match<IActionResult>(
                products => Ok(products.Adapt<ProductsResponse>()),
                _ => BadRequest("Invalid count parameter"));
        }
    }
}