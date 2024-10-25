using AutoMapper;
using E_Commerce.Dtos.Client;
using E_Commerce.Models;

namespace E_Commerce.Mappers
{
    public class ClientMapper:Profile
    {
        public ClientMapper()
        {
            CreateMap<ClientDto, Client>();
        }
    }
}
