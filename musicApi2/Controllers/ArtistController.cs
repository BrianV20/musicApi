using Microsoft.AspNetCore.Mvc;
using musicApi2.Models.Artist.Dto;
using musicApi2.Services;

namespace musicApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistInterface _artistService;

        public ArtistController(IArtistInterface artistInterface)
        {
            _artistService = artistInterface;
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ArtistDto>> GetOneById(int id)
        {
            try
            {
                var artist = await _artistService.GetOne(a => a.Id == id);
                return Ok(artist);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ArtistDto>> Create([FromBody] CreateArtistDto createArtistDto)
        {
            try
            {
                await _artistService.Add(createArtistDto);
                return Created("Create", createArtistDto);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet(Name = "GetAllArtists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetAll()
        {
            try
            {
                var artists = await _artistService.GetAll();
                return Ok(artists);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ArtistDto>> Update(int id, [FromBody] UpdateArtistDto updateArtistDto)
        {
            try
            {
                var artist = await _artistService.Update(id, updateArtistDto);
                return Ok(artist);
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
                await _artistService.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

