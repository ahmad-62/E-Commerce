using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos.CartItem
{
    public class CartItemDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]

        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
