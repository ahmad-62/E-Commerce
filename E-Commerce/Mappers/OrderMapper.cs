using AutoMapper;
using E_Commerce.Dtos.Order;
using E_Commerce.Models;

namespace E_Commerce.Mappers
{
    public class OrderMapper:Profile
    {
        public OrderMapper() {
            CreateMap<Order, OrderDto>()
                 .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(i => i.Product.Name).ToList())) 
                 .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate));
            CreateMap<Order, OrderDisplayDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Select(i => i.Product.Name).ToList())) // Map product names from OrderItem
            .ForMember(dest => dest.client, opt => opt.MapFrom(src => src.client.FullName)); // Map client name
        }
    }
    }
