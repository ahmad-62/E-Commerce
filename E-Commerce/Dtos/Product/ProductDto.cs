using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos.Product
{
    public class ProductDto
    {
        
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(50, ErrorMessage = "Brand name cannot exceed 50 characters.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "InStock must be a non-negative integer.")]
        public int InStock { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
      
        public int CategoryId { get; set; }
    }
}
