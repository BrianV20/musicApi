using Microsoft.AspNetCore.Mvc;
using musicApi2.Models.Artist;
using musicApi2.Models.Review.Dto;
using musicApi2.Services;

namespace musicApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReviewDto>> GetOneById(int id)
        {
            try
            {
                var review = await _reviewService.GetOne(a => a.Id == id);
                if (review == null)
                {
                    return NotFound("No existe un review con tal id.");
                }
                return Ok(review);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost("{reviewInfo}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReviewDto>> Create(string reviewInfo)
        {
            try
            {
                var userId = int.Parse(reviewInfo.Split("-+-+-+-")[0]);
                var releaseId = int.Parse(reviewInfo.Split("-+-+-+-")[1]);
                var reviewText = reviewInfo.Split("-+-+-+-")[2];
                var review = await _reviewService.Add(userId, releaseId, reviewText);
                //return Created("Create", review);
                return Ok(review);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet(Name = "GetAllReviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll()
        {
            try
            {
                var reviews = await _reviewService.GetAll();
                return Ok(reviews);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReviewDto>> Update(int id, [FromBody] UpdateReviewDto updateReviewDto)
        {
            try
            {
                var review = await _reviewService.Update(id, updateReviewDto);
                if (review == null)
                {
                    return NotFound("No se encontró un review con tal id");
                }
                return Ok(review);
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
                await _reviewService.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
