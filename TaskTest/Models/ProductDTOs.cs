using System.ComponentModel.DataAnnotations;

namespace TaskTest.API.Models
{

    public class ProductResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }


        //public bool? InStock { get; set; }
    }

    public class ProductListItemDto : ProductResponseDto
    {
        public bool? InStock { get; set; }
    }

    public class CreateProductRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public required string Category { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }


    public class UpdateProductRequest
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
    }

    public class ProductFilterParams
    {
        public string? Category { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }



}