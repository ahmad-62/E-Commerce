using E_Commerce.Contracts.unitofwork;
using E_Commerce.Dtos.WishList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class wishListController : ControllerBase
    {
        private readonly IUnitofwork unitOfWork;
        public wishListController(IUnitofwork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddWishlist(AddWishListDto wishListDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try {

                var wishlist = await unitOfWork.wishListService.Add(wishListDto);
                if (wishlist is null) return BadRequest();
                await unitOfWork.complete();

                return Ok(wishListDto);

            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        #region This if u need all wishlists that have created in this website
        //[HttpGet]
        //[Authorize(Roles = "Admin")]

        //public async Task<IActionResult> GetAll()
        //{
        //    try {
        //        var wishlists = await unitOfWork.wishListService.GetAll();
        //        return Ok(wishlists);

        //    }
        //    catch (Exception ex) {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        #endregion

        [HttpGet("GetAll")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> GetUserWishlists() {
            try
            {
                var wishlists = await unitOfWork.wishListService.GetAllforuser();
                if(wishlists is null) return BadRequest();
                return Ok(wishlists


             
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> UpdateWishList(AddWishListDto wishListDto, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try {
                var wishlist = await unitOfWork.wishListService.Update(wishListDto, id);
                if (wishlist is null)
                    return NotFound();
                await unitOfWork.wishListService.Update(wishListDto, id);
                await unitOfWork.complete();
                return Ok(wishListDto);



            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> Delete(int id)
        {
            try {
                var r = await unitOfWork.wishListService.Delete(id);
                if (!r)
                    return NotFound();
                await unitOfWork.wishListService.Delete(id);
                await unitOfWork.complete();
                return Ok("Deleted");

            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Additemtowishlist")]
        public async Task<IActionResult> AdditemTowishlist(int wishlistid,int productid)
        {
            try { 
            var result=await unitOfWork.wishListItemService.AddWishListItem(productid, wishlistid);
                if(result is null)
                    return NotFound();
                await unitOfWork.complete();
                    return Ok(result);
            
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost("RemoveItem")]
        public async Task<IActionResult> RemoveItem(int wishlistid, int productid)
        {
            try
            {
                var result = await unitOfWork.wishListItemService.RemoveItem(productid, wishlistid);
                if (!result)
                    return NotFound();
                await unitOfWork.complete();
                return Ok("Deleted");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
