using E_Commerce.Enums;

namespace E_Commerce.Dtos.Order
{
    public class OrderDisplayDto
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public Status Status { get; set; } = Status.Appending;

        public string client { get; set; }


        public List<string> Items { get; set; } = new List<string>();
    }
}
