﻿using Microsoft.AspNetCore.Mvc;
using musicApi2.Models.User;
using musicApi2.Models.User.Dto;
using musicApi2.Services;

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
        public async Task<ActionResult<User>> Get(int id) //FUNCIONA
        {
            try
            {
                return await _userService.GetOne(u => u.Id == id); 
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetAll() //FUNCIONA
        {
            try
            {
                var users = await _userService.GetAll();
                //var usersDto = 
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
        public async Task<ActionResult<IEnumerable<UserDto>>> Post([FromBody] CreateUserDto createUserDto) //FUNCIONA
        {
            try
            {
                var user = await _userService.Add(createUserDto);
                return Created("AddUser", user);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut(Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Put(int id, [FromBody] UpdateUserDto updateUserDto) //FUNCIONA
        {
            try
            {
                var user = await _userService.Update(id, updateUserDto);
                return user;
            }
            catch
            {
                return BadRequest();
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


        // HACER UPDATE, DELETE.
    }
}