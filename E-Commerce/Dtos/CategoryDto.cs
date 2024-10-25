using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Commerce.Dtos
{
    public class CategoryDto
    {
    
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain alphabetic characters.")]

        public string Name { get; set; }

        [MaxLength(200)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Descripition can only contain alphabetic characters.")]

        public string Description { get; set; }
    }
}
