using Microsoft.AspNetCore.Mvc;
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


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RatingDto>> GetOneById(int id)
        {
            try
            {
                var rating = await _ratingService.GetOne(a => a.Id == id);
                return Ok(rating);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RatingDto>> Create([FromBody] CreateRatingDto createRatingDto)
        {
            try
            {
                await _ratingService.Add(createRatingDto);
                return Created("Create", createRatingDto);
            }
            catch
            {
                return BadRequest();
            }
        }


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


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RatingDto>> Update(int id, [FromBody] UpdateRatingDto updateRatingDto)
        {
            try
            {
                var rating = await _ratingService.Update(id, updateRatingDto);
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
            catch
            {
                return BadRequest();
            }
        }
    }
}
