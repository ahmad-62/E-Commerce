using E_Commerce.Enums;

namespace E_Commerce.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; private set; }
        public TransactionStatus transactionstatus { get; set; }
        public PaymentMethod paymentmethod { get; set; }
        public decimal Amount { get; set; }
        public int clientId {  get; set; }
        public Client client { get; set; }
        public int OrderID {  get; set; }
        public Order order { get; set; }
        public Transaction()
        {
            TransactionDate = DateTime.UtcNow;
        }
    }
}
