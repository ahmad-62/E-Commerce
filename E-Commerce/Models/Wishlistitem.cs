using System.Text.Json.Serialization;

namespace E_Commerce.Models
{
    public class Wishlistitem
    {
        public int ProductId {  get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public int whishlistId {  get; set; }
        [JsonIgnore]
        public WishList whishlist { get; set; }
    }
}
