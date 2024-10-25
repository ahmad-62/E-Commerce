using AutoMapper;
using E_Commerce.Dtos.Product;
using E_Commerce.Models;

namespace E_Commerce.Mappers
{
    public class ProductMapper:Profile
    {

        public ProductMapper() {
        CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Product, ProductDisplayDto>()
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<ProductDto, ProductDisplayDto>().ReverseMap();
        }

    }
    
}
