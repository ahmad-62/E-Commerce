using E_Commerce.Models;

namespace E_Commerce.Dtos.WishList
{
    public class WishListDto
    {
        public string name {  get; set; }
        public List<string> products { get; set; } = new List<string>();
    }
}
