﻿using Microsoft.AspNetCore.Mvc;
using musicApi2.Models.Artist;
using musicApi2.Models.Release.Dto;
using musicApi2.Services;

namespace musicApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReleaseController : ControllerBase
    {
        private readonly IReleaseService _releaseService;

        public ReleaseController(IReleaseService releaseService)
        {
            _releaseService = releaseService;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReleaseDto>> GetOneById(int id)
        {
            try
            {
                var release = await _releaseService.GetOne(a => a.Id == id);
                if (release == null)
                {
                    return NotFound("No existe un release con tal id.");
                }
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
        public async Task<ActionResult<ReleaseDto>> Create([FromBody] CreateReleaseDto createReleaseDto)
        {
            try
            {
                await _releaseService.Add(createReleaseDto);
                return Created("Create", createReleaseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet(Name = "GetAllReleases")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ReleaseDto>>> GetAll()
        {
            try
            {
                var releases = await _releaseService.GetAll();
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
        public async Task<ActionResult<ReleaseDto>> Update(int id, [FromBody] UpdateReleaseDto updateReleaseDto)
        {
            try
            {
                var release = await _releaseService.Update(id, updateReleaseDto);
                if (release == null)
                {
                    return NotFound("No se encontró un release con tal id");
                }
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
                await _releaseService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getGenresOfRelease/{releaseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> getGenresOfRelease(int releaseId)
        {
            try
            {
                var genresOfRelease = await _releaseService.getGenresOfRelease(releaseId);
                return Ok(genresOfRelease);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
