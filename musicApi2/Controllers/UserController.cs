﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using musicApi2.Models.Artist;
using musicApi2.Models.User.Dto;
using musicApi2.Services;
using System.IdentityModel.Tokens.Jwt;

namespace musicApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        //private readonly IEntityInterface<User> _userService;
        private readonly IUserInterface _userService;

        public UserController(IUserInterface userService)
        {
            _userService = userService;
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetOneById(int id) //FUNCIONA
        {
            try
            {
                var user = await _userService.GetOne(u => u.Id == id);
                if (user == null)
                {
                    return NotFound("No existe un user con tal id.");
                }
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll() //FUNCIONA
        {
            try
            {
                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost(Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserDto>>> Create([FromBody] CreateUserDto createUserDto) //FUNCIONA
        {
            try
            {
                var user = await _userService.Add(createUserDto);
                return Created("AddUser", user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UpdateUserDto updateUserDto) //FUNCIONA
        {
            try
            {
                var user = await _userService.Update(id, updateUserDto);
                if (user == null)
                {
                    return NotFound("No se encontró un user con tal id");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id) //FUNCIONA
        {
            try
            {
                await _userService.Delete(id);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost("login", Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                var token = await _userService.Login(loginUserDto);
                if (string.IsNullOrEmpty(token))
                {
                    return NotFound("No se encontró un user con tal email y password");
                }
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{likeInfo}", Name = "LikeRelease")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LikeRelease(string likeInfo)
        {
            try
            {
                var userId = int.Parse(likeInfo.Split("+-+-+-")[0]);
                var releaseId = int.Parse(likeInfo.Split("+-+-+-")[1]);
                var result = await _userService.LikeRelease(userId, releaseId);
                if(result != null)
                {
                    return Ok(result);
                }
                return null;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLikedReleasesByUserId/{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GetLikedReleasesByUserId(int userId)
        {
            try
            {
                var result = await _userService.GetLikedReleasesByUserId(userId);
                if(result != null)
                {
                    return Ok(result);
                }
                return null;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserFromToken", Name = "GetUserFromToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GetUserFromToken()
        {
            try
            {
                if(!Request.Headers.ContainsKey("Authorization"))
                {
                    return BadRequest("No token provided");
                }
                var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                //"VXN1YXJpbyAxOmNvbnRyYXNl8WF1c3VhcmlvMQ=="
                //"QnJpYW5WNzQ1NTpwYXNzd29yZHVzZXI0"
                var validatedToken = await _userService.verifyToken(token);
                if (validatedToken != null)
                {
                    var user = await _userService.getUserFromToken(validatedToken);
                    return Ok(user);
                }
                return BadRequest("Invalid token");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateUserFavoriteReleases/{updateInfo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> updateUserFavoriteReleases(string updateInfo)
        {
            try
            {
                var userId = int.Parse(updateInfo.Split("+-+-+-")[0]);
                var releasesIds = updateInfo.Split("+-+-+-")[1];
                var result = await _userService.updateUserFavoriteReleases(userId, releasesIds);
                if(result != null)
                {
                    return Ok(result);
                }
                return BadRequest("No se pudo actualizar el usuario");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserFavoriteReleases/{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GetUserFavoriteReleases(int userId)
        {
            try
            {
                var result = await _userService.GetUserFavoriteReleases(userId);
                if(result != null)
                {
                    return Ok(result);
                }
                return null;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
