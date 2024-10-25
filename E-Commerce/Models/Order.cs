using E_Commerce.Enums;

namespace E_Commerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount => Items.Sum(Item => (Item.Product.price) * (Item.quantity));
        public Status Status { get; set; }=Status.Appending;
        public List<OrderItem> Items { get; set;} = new List<OrderItem>();
        public int ClientId { get; set; }
        public Client client { get; set; }
        public List<Transaction> transactions { get; set; }= new List<Transaction>();   
        public Order()
        {
            OrderDate = DateTime.Now;
        }
    }
}
