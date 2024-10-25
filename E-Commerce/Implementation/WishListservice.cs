using AutoMapper;
using E_Commerce.Contracts;
using E_Commerce.Contracts.Base;
using E_Commerce.Dtos.WishList;
using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Implementation
{
    public class WishListservice : IWishListService
    {
        private readonly IBaseRepository<WishList> repository;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        public WishListservice(IBaseRepository<WishList> repository,IMapper mapper,IAccountService accountService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task<WishList> Add(AddWishListDto wishListDto)
        {
            try {
                
                var client = await accountService.GetClientByAppUser();
                if (client is null)
                        return null;
                var wishlist=mapper.Map<WishList>(wishListDto);
                wishlist.clientId = client.Id; 
               var result=  await repository.Add(wishlist);
                return result;




            }
            catch (Exception ex) {
            
            throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<WishListDisplayDto>> GetAll()
        {
            try {

                var wishlists = await repository.Get2(null,
 x => x.Include(y => y.Wishlists).ThenInclude(x => x.Product));
              return mapper.Map<IEnumerable<WishListDisplayDto>>(wishlists);
          
            }
            catch(Exception ex) {

                throw new Exception(ex.Message,ex);
            }
        }

        public async Task<IEnumerable<WishListDto>> GetAllforuser()
        {
            try { 
            var client=await accountService.GetClientByAppUser();
                if (client is null)
                    return null;
                var wishlists = await repository.Get2(x=>x.clientId== client.Id,x=>x.Include(y=>y.Wishlists).ThenInclude(f=>f.Product));
             return mapper.Map<IEnumerable<WishListDto>>(wishlists);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }

        public async Task<WishListDisplayDto> GetWishList(int id)
        {
            try {
                var wishlist = await repository.Find(
                    x => x.Id == id,
                    q => q.Include(w => w.Wishlists).ThenInclude(wi => wi.Product) 
                );
                if (wishlist is null)
                    return null;
                return mapper.Map<WishListDisplayDto>(wishlist);
            
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message,ex); 
            }
        }
        public async Task<bool>CheckWishList(WishList wishlist)
        {
            var client = await accountService.GetClientByAppUser();
            if (client is null)
                return false;
            var r = client.wishlists.FirstOrDefault(x=>x.Id==wishlist.Id);
            if (r is null)
                return false;
            return true;        




        }

        public async Task<WishList> Update(AddWishListDto wishListDto, int id)
        {
            try {
               
                var _wishlist = await repository.GetById(id);
                if (_wishlist is null)
                    return null;
                var wishlist= mapper.Map(wishListDto, _wishlist);
                var r =await  CheckWishList(wishlist);
                    if(r)
                {
                    repository.Update(wishlist);
                    return wishlist;
                }
                return null;











            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message,ex);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try {
                var _wishlist = await repository.GetById(id);
                if (_wishlist is null)
                    return false;
                var r = await CheckWishList(_wishlist);
                if (r)
                    repository.Delete(_wishlist);
                return r;
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
