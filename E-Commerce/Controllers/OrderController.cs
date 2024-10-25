using E_Commerce.Contracts.unitofwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitofwork unitofwork;
        public OrderController(IUnitofwork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddOrder()
        {
            try
            {
                var orderDto = await unitofwork.orderService.Addorder();
                if (orderDto == null)
                {
                    return BadRequest("Order creation failed.");
                }
                await unitofwork.complete();
                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try
            {
                var orders = await unitofwork.orderService.GetAll();
                if (orders is null)
                    return NotFound();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try { 
            var order=await unitofwork.orderService.GeTById(id); 
                if(order is null)
                    return NotFound();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [Authorize(Roles ="User")]
        [HttpGet("GetUserOrders")]
        public async Task<IActionResult> GetUserOrders()
        {
            try {
                var orders=await unitofwork.orderService.GetAllForUsers();
                if(orders is null)
                    return NotFound();
                return Ok(orders);
            
            
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [Authorize(Roles ="User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
            
            var result=await unitofwork.orderService.Delete(id);
                if(!result)
                    return NotFound();

                await unitofwork.complete();
                return Ok("Deleted");
            
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        
        }

    }
}
