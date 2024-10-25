using E_Commerce.Contracts;
using E_Commerce.Contracts.Base;
using E_Commerce.Models;

namespace E_Commerce.Implementation
{
    public class OrderItemService:IOrderItemService
    {
        private readonly IBaseRepository<OrderItem> repository;
        public OrderItemService(IBaseRepository<OrderItem> repository) {
        this.repository = repository;
        
        }

        public async Task<OrderItem> Add(OrderItem item)
        {
            try { 
           return await repository.Add(item);
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
