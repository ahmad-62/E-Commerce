using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Client
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [StringLength(11,MinimumLength =11)]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        public string UserName {  get; set; }
        [NotMapped]
        public string FullName =>FirstName + " " + LastName;
        public List<Order> orders { get; set; } = new List<Order>();
        
        public List<WishList> wishlists { get; set; }
        public List<Transaction> Transactions { get; set; }=new List<Transaction>();
        public string ApplicationUserId {  get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
    public ApplicationUser applicationUser { get; set; }
        public ShoppingCart shoppingCart { get; set; }

    }
}
