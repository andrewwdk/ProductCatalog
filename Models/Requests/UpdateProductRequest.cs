namespace ProductCatalog.Models.Requests
{
    public record UpdateProductRequest
    (
        string Name,
        string? Description,
        decimal Price
    );
}
