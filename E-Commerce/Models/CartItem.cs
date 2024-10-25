using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity {  get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public int cartId {  get; set; }
        [ForeignKey("cartId")]
        public ShoppingCart ShoppingCart { get; set; }
        public static implicit operator OrderItem (CartItem cartItem)
        {
            if (cartItem == null)
                throw new ArgumentNullException(nameof(cartItem));

            OrderItem orderItem = new OrderItem ();
            orderItem.productId = cartItem.ProductId;
            orderItem.quantity = cartItem.Quantity;
            orderItem.Product=cartItem.Product;
         
           

            return orderItem;
        }
    }
}
