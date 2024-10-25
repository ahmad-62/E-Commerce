using AutoMapper;
using E_Commerce.Contracts;
using E_Commerce.Contracts.Base;
using E_Commerce.Dtos.WishList;
using E_Commerce.Models;
using E_Commerce.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Implementation
{
    public class WishListItemservice : IWishListItemService
    {
        private readonly AppDbContext context;
        private readonly IBaseRepository<Wishlistitem> repository;
        private readonly IWishListService wishListservice;
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public WishListItemservice(IBaseRepository<Wishlistitem> repository, IWishListService wishListservice, IMapper mapper, AppDbContext context, IAccountService accountService)
        {
            this.repository = repository;
            this.wishListservice = wishListservice;
            this.mapper = mapper;
            this.context = context;
            this.accountService = accountService;
        }

        public async Task<Wishlistitem> AddWishListItem(int productId, int wishListId)
        {
            try
            {
                var client = await accountService.GetClientByAppUser();
                if (client == null) return null;

                if (!client.wishlists.Any())
                {
                    var wishlist = new AddWishListDto
                    {
                        Name = "default",
                        Wishlists = new List<Wishlistitem> { new Wishlistitem { ProductId = productId } }
                    };

                    await wishListservice.Add(wishlist);
                    return wishlist.Wishlists.First();
                }
                else
                {

                    var wishlistDto = await wishListservice.GetWishList(wishListId);
                    var wishlist = mapper.Map<WishList>(wishlistDto);

                    var isValid = await wishListservice.CheckWishList(wishlist);
                    if (!isValid) return null;

                    var existingItem = wishlist.Wishlists.FirstOrDefault(w => w.ProductId == productId);
                    if (existingItem != null) return existingItem;
                    context.ChangeTracker.Clear();


                    //var trackedEntity = context.ChangeTracker.Entries<Wishlistitem>()
                    //                           .FirstOrDefault(e => e.Entity.ProductId == productId && e.Entity.whishlistId == wishListId);

                    //if (trackedEntity != null)
                    //{
                    //    context.Entry(trackedEntity.Entity).State = EntityState.Detached; 
                    //}

                    var wishlistitem = new Wishlistitem { ProductId = productId, whishlistId = wishListId };

                    return await repository.Add(wishlistitem); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding wishlist item: " + ex.Message, ex);
            }
        }

        public async Task<bool> RemoveItem(int productId, int wishlistId)
        {
            try
            {
                    var wishlistDto = await wishListservice.GetWishList(wishlistId);
                var wishlist = mapper.Map<WishList>(wishlistDto);

                var isValid = await wishListservice.CheckWishList(wishlist);
                if (!isValid) return false;

                var existingItem = wishlist.Wishlists.FirstOrDefault(x => x.ProductId == productId);
                if (existingItem == null) return false;
                context.ChangeTracker.Clear();


                repository.Delete(new Wishlistitem { ProductId = productId, whishlistId = wishlistId });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing wishlist item: " + ex.Message, ex);
            }
        }
    }
}
