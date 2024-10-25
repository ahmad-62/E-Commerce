using E_Commerce.Dtos.Account;
using E_Commerce.Helpers;
using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Contracts
{
    public interface IAccountService
    {
         Task<IdentityResult> CreateAppuser(RegisterDto registerDto);
        Task<IdentityResult> AssignRole(ApplicationUser user,string Role);
       Task<response> Register(RegisterDto registerDto,string role);
        Task<response> CreateRole(String RoleName);
       Task Signout();
        Task<response> login(LoginDto loginDto);
        Task<Client> GetClientByAppUser();

        //  Task<response> Addadmin(RegisterDto registerDto);
    }
}
