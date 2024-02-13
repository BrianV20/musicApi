using Microsoft.AspNetCore.Mvc;
using musicApi2.Models.Artist;
using musicApi2.Models.Rating.Dto;
using musicApi2.Services;

namespace musicApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingInterface _ratingService;

        public RatingController(IRatingInterface ratingService)
        {
            _ratingService = ratingService;
        }


        [HttpGet("{userAndReleaseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RatingDto>> GetOneById(string userAndReleaseId)
        {
            try
            {
                var userId = int.Parse(userAndReleaseId.Split("-")[0]);
                var releaseId = int.Parse(userAndReleaseId.Split("-")[1]);
                var rating = await _ratingService.GetOne(a => a.UserId == userId && a.ReleaseId == releaseId);
                if (rating == null)
                {
                    return BadRequest("No se encontró un rating con tal id.");
                }
                return Ok(rating);
            }
            catch
            {
                return BadRequest();
            }
        }


        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<RatingDto>> Create([FromBody] CreateRatingDto createRatingDto)
        //{
        //    try
        //    {
        //        await _ratingService.Add(createRatingDto);
        //        return Created("Create", createRatingDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpGet(Name = "GetAllRatings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetAll()
        {
            try
            {
                var ratings = await _ratingService.GetAll();
                return Ok(ratings);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("{userAndReleaseIdAndRating}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<RatingDto>> Update(int id, [FromBody] UpdateRatingDto updateRatingDto)
        public async Task<ActionResult<RatingDto>> Update(string userAndReleaseIdAndRating)
        {
            try
            {
                //var rating = await _ratingService.Update(id, updateRatingDto);
                var userId = int.Parse(userAndReleaseIdAndRating.Split("-")[0]);
                var releaseId = int.Parse(userAndReleaseIdAndRating.Split("-")[1]);
                var newRating = userAndReleaseIdAndRating.Split("-")[2];
                var rating = await _ratingService.Update(userId, releaseId, newRating);
                if (rating == null)
                {
                    return NotFound("No se encontró un rating con tal id");
                }
                return Ok(rating);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _ratingService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
