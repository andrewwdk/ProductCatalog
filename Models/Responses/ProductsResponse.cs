using ProductCatalog.Models.Dto;

namespace ProductCatalog.Models.Responses
{
    public record ProductsResponse
    (
        List<ProductDto> Products
    );
}
