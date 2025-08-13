using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Product : Entity
    {
        


        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Category { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

        public double? Rating { get; set; }

        public bool? IsNew { get; set; }
    }
}
