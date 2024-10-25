using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        public string username {  get; set; }
        [Required]
        public string password { get; set; }    
    }
}
