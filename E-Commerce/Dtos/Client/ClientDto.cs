using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos.Client
{
    public class ClientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }   
        public string ApplicationuserId {  get; set; }
        public string UserName {  get; set; }
    }
}
