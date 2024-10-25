using E_Commerce.Dtos.Client;
using E_Commerce.Models;

namespace E_Commerce.Contracts
{
    public interface IClientService
    {
        Task Add(ClientDto client);
        Task<Client>GetbyAppUserId(string appUserId);
    }
}
