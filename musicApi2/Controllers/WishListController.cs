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

        [HttpGet("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WishListDto>> GetByUserId(int userId)
        {
            try
            {
                var wishlist = await _wishListService.GetOne(w => w.userId == userId);
                if (wishlist == null)
                {
                    return null;
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
        public async Task<ActionResult> Create(int userId)
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

        //[HttpPut("AddRelease, {userId:int}, {releaseId:int}")] //BUSCAR COMO HACER BIEN ESTO, ADDRELEASE Y REMOVE RELEASE RECIBEN LOS MISMOS PARAMETROS PERO HAY QUE DIFERENCIAR AMBOS METODOS DE ALGUNA MANERA
        //[HttpPut("{userId:int}-{releaseId:int}", Name = "AddRelease")]
        [HttpPut("AddRelease/{userAndReleaseId}", Name = "AddRelease")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddRelease(string userAndReleaseId)
        {
            try
            {
                var ids = userAndReleaseId.ToString().Split('-');
                var userId = int.Parse(ids[0]);
                var releaseId = int.Parse(ids[1]);
                await _wishListService.AddRelease(userId, releaseId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("RemoveRelease/{userAndReleaseId}", Name = "RemoveRelease")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveRelease(string userAndReleaseId)
        {
            try
            {
                var ids = userAndReleaseId.ToString().Split('-');
                var userId = int.Parse(ids[0]);
                var releaseId = int.Parse(ids[1]);
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
