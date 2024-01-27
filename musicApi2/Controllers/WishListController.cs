using Microsoft.AspNetCore.Mvc;
using musicApi2.Models.WishList.Dto;
using musicApi2.Services;

namespace musicApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListInterface _wishListService;

        public WishListController(IWishListInterface wishListService)
        {
            _wishListService = wishListService;
        }

        [HttpGet("{wishlistId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WishListDto>> GetOneById(int wishlistId)
        {
            try
            {
                var wishlist = await _wishListService.GetOne(w => w.Id == wishlistId);
                if (wishlist == null)
                {
                    return NotFound("No existe un wishlist con tal id.");
                }
                return Ok(wishlist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(Name = "GetAllWishlists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<WishListDto>>> GetAll()
        {
            try
            {
                var wishlists = await _wishListService.GetAll();
                return Ok(wishlists);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(Name = "AddWishlist")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create (int userId)
        {
            try
            {
                await _wishListService.Create(userId);
                return Created("Added wishlist", null);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("AddRelease", Name = "AddRelease")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddRelease(int userId, int releaseId)
        {
            try
            {
                await _wishListService.AddRelease(userId, releaseId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("RemoveRelease", Name = "RemoveRelease")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveRelease(int userId, int releaseId)
        {
            try
            {
                await _wishListService.RemoveRelease(userId, releaseId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
