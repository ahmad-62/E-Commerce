    namespace E_Commerce.Models
    {
        public class WishList
        {
            public int Id { get; set; }
            public string Name { get; set; }
           public List<Wishlistitem> Wishlists { get; set; } = new List<Wishlistitem>();
            public int clientId {  get; set; }
            public Client Client { get; set; }
        }
    }
