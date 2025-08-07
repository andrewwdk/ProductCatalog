namespace ProductCatalog.Models.Dto
{
    public class UpdateProductDto
    {
        public string Name {  get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
