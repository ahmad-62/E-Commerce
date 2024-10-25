using E_Commerce.Contracts.unitofwork;
using E_Commerce.Dtos.CartItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]

    public class CartController : ControllerBase
    {
        private readonly IUnitofwork unitofwork;
        public CartController(IUnitofwork unitofwork)
        {
            this.unitofwork = unitofwork;
        
        }
        [HttpGet]
        public async  Task<IActionResult> Get()
        {
            try { 
            var cart=await unitofwork.cartService.GetCartItemAsync();
                if(cart is null) 
                    return NotFound();
            return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpPost("Additemtocart")]
        public async Task <IActionResult> AddItem(AddCartItemDto itemDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try { 
            var item=await unitofwork.cartItemService.AddItemTocart(itemDto);
                if (item is null)
                    return NotFound();
                await unitofwork.complete();

                return Ok(item);

            
            
            
            
            
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        
        
        }
        [HttpPost("RemoveItem")]
        public async Task<IActionResult> RemoveItem(int productid)
        {
            try {
                var result=await unitofwork.cartItemService.Removeitemfromcart(productid);
                if(!result)
                    return NotFound();
                await unitofwork.complete();
                return Ok("removed");
            
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

    }
}
