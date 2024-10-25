using E_Commerce.Models;

namespace E_Commerce.Contracts.Account
{
    public interface ITokenService
    {
         Task<string> CreateTokenAsync(ApplicationUser user);

    }
}
