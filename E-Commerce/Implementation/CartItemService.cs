using AutoMapper;
using E_Commerce.Contracts;
using E_Commerce.Contracts.Base;
using E_Commerce.Dtos.CartItem;
using E_Commerce.Models;

namespace E_Commerce.Implementation
{
    public class CartItemService : ICartItemService
    {
        private readonly IBaseRepository<CartItem> repository;
        private readonly IAccountService accountService;
        private readonly ICartService cartService;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public CartItemService(IBaseRepository<CartItem> repository, IAccountService accountService, ICartService cartService, IMapper mapper, IProductService productService)
        {
            this.repository = repository;
            this.accountService = accountService;
            this.cartService = cartService;
            this.mapper = mapper;
            this.productService = productService;
        }

        public async Task<CartItemDto> AddItemTocart(AddCartItemDto item)
        {
            try
            {
                var client = await accountService.GetClientByAppUser();
                if (client is null)
                    return null;

                var product = await productService.GetById(item.ProductId);
                if (product is null || product.InStock <= 0)
                    throw new Exception("Product Out of stock");

                if (client.shoppingCart is null)
                {
                    var newCartItem = new CartItem { Quantity = 1, ProductId = item.ProductId };
                    var newCart = new ShoppingCart
                    {
                        clientId = client.Id,
                        Items = new List<CartItem> { newCartItem }
                    };

                    await cartService.AddCart(newCart);
                    return mapper.Map<CartItemDto>(newCartItem);
                }
                else
                {
                    // If the cart exists, add the item or update its quantity
                    var existingItem = client.shoppingCart.Items.FirstOrDefault(x => x.ProductId == item.ProductId);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += 1;
                        if(existingItem.Quantity > product.InStock)
                        {
                            existingItem.Quantity = product.InStock;
                        }
                        repository.Update(existingItem);
                        return mapper.Map<CartItemDto>(existingItem);
                    }
                    else
                    {
                        var newItem = new CartItem
                        {
                            ProductId = item.ProductId,
                            Quantity = 1,
                            cartId = client.shoppingCart.Id
                        };

                        await repository.Add(newItem);
                        return mapper.Map<CartItemDto>(newItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> Removeitemfromcart(int productId)
        {
            try
            {
                var client = await accountService.GetClientByAppUser();
                if (client is null)
                    return false;

                var existingItem = client.shoppingCart.Items.FirstOrDefault(x => x.ProductId == productId);
                if (existingItem == null)
                    throw new Exception("This product is not found in the cart.");

                existingItem.Quantity -= 1;

                if (existingItem.Quantity <= 0)
                {
                    repository.Delete(existingItem);
                }
                else
                {
                    repository.Update(existingItem);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task clearcart(int cartId)
        {
            try { 
            var items=await repository.Get(x=>x.cartId==cartId);
                if (items is null)
                    throw new Exception("There is no items in the cart");
                 repository.RemoveRange(items);
            
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
