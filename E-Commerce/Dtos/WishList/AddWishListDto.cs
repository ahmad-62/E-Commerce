using E_Commerce.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Commerce.Dtos.WishList
{
    public class AddWishListDto
    {
        [Required]
        [Length(3,15)]
        public string Name {  get; set; }
        //public int ClientId {  get; set; }
        [JsonIgnore]
        public List<Wishlistitem>? Wishlists { get; set; } = new List<Wishlistitem>();

    }
}
