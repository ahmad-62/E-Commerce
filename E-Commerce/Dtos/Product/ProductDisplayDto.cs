using System.Text.Json.Serialization;

namespace E_Commerce.Dtos.Product
{
    public class ProductDisplayDto
    {
        [JsonIgnore]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal price { get; set; }
        public int InStock { get; set; }
        public CategoryDto Category { get; set; }
    }
}
