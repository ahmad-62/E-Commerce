using AutoMapper;
using E_Commerce.Contracts;
using E_Commerce.Contracts.Base;
using E_Commerce.Dtos.Cart;
using E_Commerce.Dtos.CartItem;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Implementation
{
    public class CartService:ICartService
    {
        private readonly IBaseRepository<ShoppingCart> repository;
        
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        public CartService(IBaseRepository<ShoppingCart> repository, IAccountService     accountService,IMapper mapper)
        {
            this.repository = repository;
            this.accountService = accountService;
            this.mapper = mapper;
        }

        public Task<ShoppingCart> AddCart(ShoppingCart cart)
        {
            try { 

            return repository.Add(cart);
            
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

       

        async Task<CartDto> ICartService.GetCartItemAsync()
        {
            try {
                var client = await accountService.GetClientByAppUser();
                if (client is null)
                    return null;
                var cart=await repository.Find(x=>x.clientId==client.Id,x=>x.Include(f=>f.Items).ThenInclude(y=>y.Product));
                if (cart is null)
                    return null;
                return mapper.Map<CartDto>(cart);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
