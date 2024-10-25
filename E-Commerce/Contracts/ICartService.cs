using E_Commerce.Dtos.Cart;
using E_Commerce.Models;

namespace E_Commerce.Contracts
{
    public interface ICartService
    {
        Task<CartDto>GetCartItemAsync();
        Task<ShoppingCart> AddCart(ShoppingCart cart);
    }
}
