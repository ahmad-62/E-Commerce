using E_Commerce.Dtos.CartItem;
using E_Commerce.Models;

namespace E_Commerce.Contracts
{
    public interface ICartItemService
    {
        Task<CartItemDto> AddItemTocart(AddCartItemDto item);
        Task<bool> Removeitemfromcart(int productId);
        Task clearcart(int cartId);
    }
}
