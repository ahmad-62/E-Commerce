using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "FirstName must be 5 characters")]
        [MaxLength(15, ErrorMessage = "FirstName cannot be over 15 characters ")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "FirstName can only contain alphabetic characters.")]

        public String FirstName { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "LastName must be 5 characters")]
        [MaxLength(15, ErrorMessage = "LastName cannot be over 15 characters ")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "LastName can only contain alphabetic characters.")]

        public String LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]

        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword {  get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Address must be between 10 and 100 characters long.")]
        public string Address { get; set; }
        
        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be exactly 12 digits and contain only numbers.")]
        public string PhoneNumber { get; set; }
    }
}
