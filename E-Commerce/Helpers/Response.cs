using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Helpers
{
    public class response
    {
        public IdentityResult IdentityResult { get; set; }
        public string token {  get; set; }
    }
}
