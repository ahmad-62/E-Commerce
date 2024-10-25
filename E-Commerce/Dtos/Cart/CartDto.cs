namespace E_Commerce.Dtos.Cart
{
    public class CartDto
    {
        public decimal TotalAmount {  get; set; }
        public List<string> Items { get; set; } = new List<string>();
    }
}
