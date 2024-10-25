namespace E_Commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Brand {  get; set; }
        public decimal price {  get; set; }
        public int InStock { get; set; }
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
        public List<Wishlistitem> Whishlist { get; set; } = new List<Wishlistitem>();
        public List<OrderItem> OrderItems { get; set; }= new List<OrderItem>();
        public List<CartItem> CartItems { get; set; }=new List<CartItem>();




    }
}
