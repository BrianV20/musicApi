using Microsoft.AspNetCore.Mvc;
using musicApi2.Models.Genre.Dto;
using musicApi2.Services;

namespace musicApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenreDto>> GetOneById(int id)
        {
            try
            {
                var release = await _genreService.GetOne(a => a.Id == id);
                return Ok(release);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenreDto>> Create([FromBody] CreateGenreDto createGenreDto)
        {
            try
            {
                await _genreService.Add(createGenreDto);
                return Created("Create", createGenreDto);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet(Name = "GetAllGenres")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
        {
            try
            {
                var releases = await _genreService.GetAll();
                return Ok(releases);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenreDto>> Update(int id, [FromBody] UpdateGenreDto updateGenreDto)
        {
            try
            {
                var release = await _genreService.Update(id, updateGenreDto);
                return Ok(release);
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
                await _genreService.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
