using E_Commerce.Models;

namespace E_Commerce.Contracts
{
    public interface IOrderItemService
    {
        Task<OrderItem> Add(OrderItem item);
    }
}
