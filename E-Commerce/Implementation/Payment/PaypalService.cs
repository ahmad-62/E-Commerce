using E_Commerce.Contracts.Payment;
using E_Commerce.Enums;
using E_Commerce.Models;
using PayPalCheckoutSdk.Orders;
using PayPalCheckoutSdk.Core;
using E_Commerce.Contracts;

namespace E_Commerce.Implementation.Payment
{
    public class PaypalService : IPaymentService
    {
        private readonly PayPalHttpClient _payPalClient;
        private readonly ITransactionService _transactionService;

        public PaypalService(PayPalHttpClient payPalClient, ITransactionService transactionService)
        {
            _payPalClient = payPalClient;
            _transactionService = transactionService;
        }

        public async Task<bool> ProcessPaymentAsync(Models.Order order, decimal amount)
        {
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE", // Specify the intent as 'CAPTURE'
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest
                    {
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = "USD",
                            Value = amount.ToString()
                        }
                    }
                }
            });

            try
            {
                var response = await _payPalClient.Execute(request);
                var result = response.Result<PayPalCheckoutSdk.Orders.Order>();

                var transaction = new Models.Transaction
                {
                    Amount = amount,
                    clientId = order.client.Id,
                    OrderID = order.Id,
                    order = order,
                    paymentmethod = Enums.PaymentMethod.PayPal
                };

                if (result.Status == "COMPLETED")
                {
                    transaction.transactionstatus = TransactionStatus.Success;
                }
                else
                {
                    transaction.transactionstatus = TransactionStatus.Failed;
                }

               await _transactionService.Add(transaction);
                return result.Status == "COMPLETED";
            }
            catch (Exception ex)
            {
                var failedTransaction = new Transaction        {
                    Amount = amount,
                    transactionstatus = TransactionStatus.Failed,
                    paymentmethod = Enums.PaymentMethod.PayPal,
                    clientId = order.client.Id,
                    OrderID = order.Id,
                    order = order
                };

                await _transactionService.Add(failedTransaction);
                return false;
            }
        }
    }
}
