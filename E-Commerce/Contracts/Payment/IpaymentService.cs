using E_Commerce.Enums;
using PayPalCheckoutSdk.Orders;

namespace E_Commerce.Contracts.Payment
{
    public interface IPaymentService
    {
        // Method to process a payment for an order using PayPal
        Task<bool> ProcessPaymentAsync(Models.Order order, decimal amount);

        // Method to check the status of a payment if needed

    }
}
