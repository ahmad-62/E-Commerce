using Azure;
using E_Commerce.Contracts;
using E_Commerce.Contracts.Account;
using E_Commerce.Contracts.Base;
using E_Commerce.Dtos.Account;
using E_Commerce.Dtos.Client;
using E_Commerce.Helpers;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Implementation.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly ITokenService tokenService;
        private readonly IClientService Repository;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly SignInManager<ApplicationUser> signInManager;  
        public AccountService(UserManager<ApplicationUser> usermanager, ITokenService tokenService, IClientService Repository, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this.usermanager = usermanager;
            this.tokenService = tokenService;
            this.Repository = Repository;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> AssignRole(ApplicationUser user, string Role)
        {
            try
            {
                return await usermanager.AddToRoleAsync(user, Role);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      public  async Task<IdentityResult> CreateAppuser(RegisterDto registerDto)
        {
            try {
                var Appuser = new ApplicationUser
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                    UserName= registerDto.UserName

                };
                return await usermanager.CreateAsync(Appuser,registerDto.Password);
            
            
            
            }
            
            
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public async Task<response> Register(RegisterDto registerDto,string role)
        {
            var Response=new response();
            try {
                var createdresult = await CreateAppuser(registerDto);


                if (!createdresult.Succeeded) {
                    Response.IdentityResult= createdresult;
                    return Response;
                }
              
                var user = await usermanager.FindByNameAsync(registerDto.UserName);
                var roleResult = await AssignRole(user, role);
                if(!roleResult.Succeeded)
                {
                    Response.IdentityResult= roleResult;
                    return Response;
                }
                if (role == "User")
                {
                    var client = new ClientDto
                    {
                        Address = registerDto.Address,
                        Email = registerDto.Email,
                        ApplicationuserId = user.Id,
                        FirstName = registerDto.FirstName,
                        LastName = registerDto.LastName,
                        PhoneNumber = registerDto.PhoneNumber,
                        UserName= registerDto.UserName,


                    };
                    await Repository.Add(client);
                }
                Response.token= await tokenService.CreateTokenAsync(user);
                Response.IdentityResult = IdentityResult.Success;
                return Response;
            
            
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<response> CreateRole(string RoleName)
        {
            var Response = new response();
            try
            {

                if (await roleManager.RoleExistsAsync(RoleName))
                {
                    Response.IdentityResult = IdentityResult.Failed(new IdentityError
                    {
                        Description = "This role already exists."

                    });
                    return Response;


                }
                var Role = new IdentityRole(RoleName);
                var result = await roleManager.CreateAsync(Role);
                if (!result.Succeeded)
                {
                    Response.IdentityResult = result;
                }
                Response.IdentityResult = IdentityResult.Success;
                return Response;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
     
        public async Task Signout()
        {
            try { 
            await signInManager.SignOutAsync();
            
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<response> login(LoginDto loginDto)
        {
            var Response=new response();
            try {
                var user = await usermanager.FindByNameAsync(loginDto.username);
                if(user == null)
                {
                    Response.IdentityResult = IdentityResult.Failed(new IdentityError { Description = "There isnot user with user" });
                    return Response;
                }
                var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.password, true);
                if (!result.Succeeded)
                {
                    Response.IdentityResult = IdentityResult.Failed(new IdentityError { Description = "Invalid Password" });
                    return Response;
                }
                Response.token = await tokenService.CreateTokenAsync(user);
                Response.IdentityResult = IdentityResult.Success;
                return Response;
            }
            catch(Exception ex) { 
            
            throw new Exception(ex.Message);
            }
        }
        public async Task<Client> GetClientByAppUser()
        {
            var id = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id is null)
            {
                return null;
            }
            var client = await Repository.GetbyAppUserId(id);
            if (client is null)
                return null;
            return client;
        }
    }
}
