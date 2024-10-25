using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Commerce.Dtos.CartItem
{
    public class AddCartItemDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]

        public int ProductId { get; set; }
        [JsonIgnore]
        public int cartId { get; set; }

    }
}
