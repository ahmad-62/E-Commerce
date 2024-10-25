namespace E_Commerce.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
       

        public decimal TotalAmount=>Items.Sum(item=>(item.Product.price)*(item.Quantity));

        public List<CartItem> Items { get; set; }=new List<CartItem>();
        public int clientId {  get; set; }
        public Client client { get; set; }
    }
}
