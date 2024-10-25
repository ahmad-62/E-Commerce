using E_Commerce.Contracts.unitofwork;
using E_Commerce.Dtos.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitofwork unitofwork;
        public AccountController(IUnitofwork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                
            }
            try {
                var Response = await unitofwork.AccountManager.Register(registerDto,"User");
                if(Response.IdentityResult.Succeeded)
                {
                    await unitofwork.complete();
                    return Ok(Response.token);

                }
                var errors=Response.IdentityResult.Errors.Select(x=>x.Description).ToList();
                return BadRequest(errors);
            
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Response = await unitofwork.AccountManager.login(loginDto);
                if (Response.IdentityResult.Succeeded)
                {
                    return Ok(Response.token);

                }
                var errors = Response.IdentityResult.Errors.Select(x => x.Description);
                return BadRequest(errors);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);  
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateRole/{RoleName}")]
        public async Task<IActionResult> CreateRole(string RoleName)
        {
            try {
                if (string.IsNullOrWhiteSpace(RoleName) || RoleName.Length < 3 || RoleName.Length > 10)
                {
                    ModelState.AddModelError("RoleName", "Role name must be between 3 and 10 characters.");
                    return BadRequest(ModelState);
                }
                var result=await unitofwork.AccountManager.CreateRole(RoleName);
                if (result.IdentityResult.Succeeded) 
                return Ok("Role created successfully.");
                var Errors = result.IdentityResult.Errors.Select(x => x.Description);
               return BadRequest(Errors);


            }
            catch (Exception ex) { 
            
return BadRequest(ex.Message);            
            } 
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin(RegisterDto Admin)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try {

                var Response = await unitofwork.AccountManager.Register(Admin, "Admin");
                if(Response.IdentityResult.Succeeded)
                {
                    return Ok(Response.token);
                }
                var Errors=Response.IdentityResult.Errors.Select(x=>x.Description);
                return BadRequest(Errors);
            
            }
            catch(Exception ex) { 
            return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("Signout")]
        public async Task<IActionResult> Signout()
        {
            try{

                await unitofwork.AccountManager.Signout();
                return Ok("SignedOut");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
