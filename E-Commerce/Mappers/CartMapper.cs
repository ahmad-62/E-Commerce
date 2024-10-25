using AutoMapper;
using E_Commerce.Dtos.Cart;
using E_Commerce.Dtos.CartItem;
using E_Commerce.Models;

namespace E_Commerce.Mappers
{
    public class CartMapper:Profile
    {
        public CartMapper() {
            CreateMap<AddCartItemDto, CartItem>();
            CreateMap<ShoppingCart, CartDto>()
                .ForMember(opt => opt.Items,
                           x => x.MapFrom(y => y.Items.Select(i => i.Product.Name)));
            CreateMap<AddCartItemDto, CartItemDto>();
            CreateMap<CartItem,CartItemDto>();

        }
    }
}
