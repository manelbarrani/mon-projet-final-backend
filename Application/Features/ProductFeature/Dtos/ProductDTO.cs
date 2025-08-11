namespace Application.Features.ProductFeature.Dtos
{
    public class ProductDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

        public double? Rating { get; set; }

        public bool? IsNew { get; set; }
    }
}
