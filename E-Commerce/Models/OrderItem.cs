namespace E_Commerce.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int quantity { get; set;  }
        public int productId {  get; set; }
        public Product Product { get; set; }
        public int orderId {  get; set; }   
        public Order Order { get; set; }

    }
}
