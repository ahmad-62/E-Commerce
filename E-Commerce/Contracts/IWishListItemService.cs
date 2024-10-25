using E_Commerce.Models;

namespace E_Commerce.Contracts
{
    public interface IWishListItemService
    {
        Task<Wishlistitem> AddWishListItem(int productId,int wishLisId);
        Task<bool> RemoveItem(int productId,int wishlistId);

    }
}
