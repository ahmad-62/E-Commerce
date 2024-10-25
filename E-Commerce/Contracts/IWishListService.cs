using E_Commerce.Dtos.WishList;
using E_Commerce.Models;

namespace E_Commerce.Contracts
{
    public interface IWishListService
    {
        Task<IEnumerable<WishListDisplayDto>> GetAll();
        Task<IEnumerable<WishListDto>> GetAllforuser();
        Task<WishListDisplayDto> GetWishList(int id);
        Task<WishList> Update(AddWishListDto whishListDto, int id);
        Task<WishList> Add(AddWishListDto whishListDto);
        Task<bool>Delete(int id);
        Task<bool> CheckWishList(WishList wishlist);
        
    }
}
