using AutoMapper;
using E_Commerce.Dtos;
using E_Commerce.Models;

namespace E_Commerce.Mappers
{
    public class CategoryMapper :Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category,CategoryDto>().ReverseMap();

        }
    }
}
