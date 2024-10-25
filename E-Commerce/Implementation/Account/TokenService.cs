using E_Commerce.Contracts.Account;
using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Implementation.TokenService
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> userManager;
        public TokenService(IConfiguration config,UserManager<ApplicationUser> userManager)
        {
            _config = config;
            this.userManager = userManager;


        }

        public async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.FullName),
                

            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var SignInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audiance"],

                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: SignInCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
