using AutoMapper;
using E_Commerce.Contracts.unitofwork;
using E_Commerce.Dtos.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitofwork unitofwork;
        private readonly IMapper mapper;
        public ProductController(IUnitofwork unitofwork, IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try {
                var products = await unitofwork.products.GetAll();


                return Ok(mapper.Map<IEnumerable<ProductDisplayDto>>(products));

            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try {
                var product = await unitofwork.products.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<ProductDisplayDto>(product));


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> add(ProductDto product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try {

                var createdproduct = await unitofwork.products.Add(product);
                await unitofwork.complete();
                return Ok(product);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]  
         [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update([FromRoute] int id, ProductDto Newproduct)
        {
            if (!ModelState.IsValid)

                return BadRequest(ModelState);


            try
            {

                var oldproduct = await unitofwork.products.Update(Newproduct, id);

                if (oldproduct == null)
                    return NotFound();
                await unitofwork.complete();
                return Ok(Newproduct);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result=await unitofwork.products.Delete(id);
                if(!result)
                    return NotFound();
                await unitofwork.complete();
                return Ok("Deleted");
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);

            }
        }
    } 

    
}
