using E_Commerce.Dtos.Order;
using E_Commerce.Models;
using System.Collections;

namespace E_Commerce.Contracts
{
    public interface IOrderService
    {
        public Task<OrderDto> Addorder();
        public Task<IEnumerable<OrderDisplayDto>> GetAll();
        public Task<OrderDto> GeTById (int id);
        public Task<bool> Delete(int id);
        public Task<IEnumerable<OrderDto>> GetAllForUsers();
    }
}
