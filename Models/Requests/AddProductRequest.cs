namespace ProductCatalog.Models.Requests
{
    public record AddProductRequest
    (
        string Name,
        string? Description,
        decimal Price
    );
}
