using E_Commerce.Models;
using System.Text.Json.Serialization;

namespace E_Commerce.Dtos.WishList
{
    public class WishListDisplayDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name {  get; set; }
        public string UserName {  get; set; }
        public string CreatedBy { get; set; }
        public List<Wishlistitem> Wishlists = new List<Wishlistitem>();
        [JsonIgnore]    
        public int clientid {  get; set; }

    }
}
