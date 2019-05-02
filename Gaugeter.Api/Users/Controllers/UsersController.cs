using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Gaugeter.Api.Users.Services;
using System.ComponentModel.DataAnnotations;
using Gaugeter.Api.Users.Models.Data;
using AutoMapper;
using Gaugeter.Api.Users.Models.Dto;
using System.Collections.Generic;

namespace Gaugeter.Api.Users.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string userId)
        {
            var user = await _usersService.Get(userId);

            if (user == null)
                return NoContent();

            return Ok(_mapper.Map<User, UserDto>(user));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersService.GetAll();

            if (users == null)
                return NoContent();
                
            return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody][Required] UserDto userDto)
        {
            var user = _mapper.Map<UserDto, User>(userDto);
            
            if (await _usersService.Create(user) != EntityState.Added)
                return StatusCode(StatusCodes.Status500InternalServerError);
            
            var mappedUser = _mapper.Map<User, UserDto>(user);
            
            return CreatedAtAction(nameof(Get), new { id = user.UserId }, mappedUser);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody][Required] UserDto userDto)
        {
            var user = _mapper.Map<UserDto, User>(userDto);
            
            if (await _usersService.Update(user) != EntityState.Modified) 
                return BadRequest(ModelState);
            
            var mappedUser = _mapper.Map<User, UserDto>(user);
            
            return CreatedAtAction(nameof(Get), new { id = user.UserId }, mappedUser);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery][Required] string userId)
        {
            if (await _usersService.Delete(userId) == EntityState.Deleted)
                return Ok();

            return NoContent();
        }
    }
}
