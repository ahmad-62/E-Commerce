using E_Commerce.Enums;
using E_Commerce.Models;

namespace E_Commerce.Dtos.Order
{
    public class OrderDto
    {
        public DateTime OrderDate => DateTime.Now;
        public decimal TotalAmount { get; set; }
        public Status Status { get; set; } = Status.Appending;
       
        public List<string> Items { get; set; } = new List<string>();

    }
}
