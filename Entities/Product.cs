namespace ProductCatalog.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public static Product Create(string name, string? description, decimal price)
        {
            Validate(name, description, price);
            return new Product
            {
                Name = name,
                Description = description,
                Price = price
            };
        }

        public void Update(string name, string? description, decimal price)
        {
            Validate(name, description, price);

            Name = name;
            Description = description;
            Price = price;
        }

        private static void Validate(string name, string? description, decimal price)
        {
            // Add validation
        }
    }
}
