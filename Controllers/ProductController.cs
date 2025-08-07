using Mapster;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models.Responses;
using ProductCatalog.Services;

namespace ProductCatalog.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("~/api/products")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetProductsAsync();

            return Ok(result.Adapt<ProductsResponse>());
        }
    }
}
