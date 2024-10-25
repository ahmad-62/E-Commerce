using AutoMapper;
using E_Commerce.Contracts;
using E_Commerce.Contracts.Base;
using E_Commerce.Dtos.Client;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Implementation
{
    public class CLientService:IClientService
    {
        private readonly IBaseRepository<Client> repository;
        private readonly IMapper mapper;
        
        public CLientService(IBaseRepository<Client> repository,IMapper mapper) {
       this.repository = repository;
            this.mapper = mapper;
        
        }

        public async Task Add(ClientDto client)
        {
            try
            {
                var NewClient =mapper.Map<Client>(client);
        await repository.Add(NewClient);

            }
            catch(Exception ex) {

                throw new Exception(ex.Message);
            }

        }

        public async Task<Client> GetbyAppUserId(string appUserId)
        {
            try {
                var client = await repository.Find(x=>x.ApplicationUserId==appUserId,X=>X.Include(x=>x.wishlists),y=>y.Include(x=>x.shoppingCart).ThenInclude(x=>x.Items).ThenInclude(x=>x.Product).Include(x=>x.orders).ThenInclude(x=>x.Items));
                if (client is null)
                {
                    return null;
                }
                return client;
            
            
            
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
