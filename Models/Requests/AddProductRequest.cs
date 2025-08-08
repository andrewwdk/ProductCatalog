using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models.Requests
{
    public record AddProductRequest
    (
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(100, ErrorMessage = "Product name should be up to 100 characters")]
        [MinLength(2, ErrorMessage = "Product name should not be less than 2 characters")]
        string Name,
        [MaxLength(500, ErrorMessage = "Product description should be up to 500 characters")]
        string? Description,
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price should be a possitive value")]
        decimal Price
    );
}
