using E_Commerce.Contracts.unitofwork;
using E_Commerce.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitofwork unitofwork;
        public CategoryController(IUnitofwork unitofwork)
        {
            this.unitofwork = unitofwork;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try { 
                var categories=await unitofwork.categories.GetAll();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            try {
                var category=await unitofwork.categories.GetById(id);

            if(category is null)
                    return NotFound("Isnot Found");
            return Ok(category);

            
            
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(CategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try { 
                
           var _category= await unitofwork.categories.Add(category);
                await unitofwork.complete();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
         public async Task<IActionResult> Update(CategoryDto category,[FromRoute]int id)
        {
            if(!ModelState.IsValid) { 
            return BadRequest(ModelState);
            }
            try { 
            var _category=await unitofwork.categories.Update(category,id);
                if (_category is null)
                    return NotFound("This category Isnot found");
                await unitofwork.complete();

                return Ok(category);

            
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
         public async Task<IActionResult> Delete(int id) {
            try { 
            var result=await unitofwork.categories.Delete(id);
                if (!result)
                    return NotFound("This category Isnot found");
                await unitofwork.complete();
                return Ok("Deleted");
            
            
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);

            }


        }
    }
}
