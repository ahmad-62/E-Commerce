using AutoMapper;
using E_Commerce.Dtos.WishList;
using E_Commerce.Models;

namespace E_Commerce.Mappers
{
    public class WishListMapper:Profile
    {
        public WishListMapper()
        {
            CreateMap<AddWishListDto, WishList>();
            CreateMap<WishList, WishListDisplayDto>().ForMember(opt=>opt.UserName,opt=>opt.MapFrom(x=>x.Client.UserName)).ForMember(opt=>opt.CreatedBy,opt=>opt.MapFrom(x=>x.Client.FullName)).ReverseMap();
            CreateMap<WishList, WishListDto>()
       // Use the correct navigation property here: WishListItems instead of Wishlists
       .ForMember(dest => dest.products, opt => opt.MapFrom(src => src.Wishlists.Select(wi => wi.Product.Name)));

            CreateMap<WishListDisplayDto, WishListDto>();
            
        }
    }
}
