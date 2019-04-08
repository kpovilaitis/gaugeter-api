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
            var user = await _usersService.GetUser(userId);

            if (user == null)
                return NoContent();

            return Ok(_mapper.Map<User, UserDto>(user));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _usersService.GetAllUsers();

            if (users == null)
                return NoContent();
                
            return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody][Required] User user)
        {
            if (await _usersService.CreateUser(user) == EntityState.Added)
            {
                var mappedUser = _mapper.Map<User, UserDto>(user);
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, mappedUser);
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody][Required] User user)
        {
            if (await _usersService.UpdateUser(user) == EntityState.Modified)
            {
                var mappedUser = _mapper.Map<User, UserDto>(user);
                return CreatedAtAction(nameof(Get), new { id = user.UserId }, mappedUser);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute][Required] string userId)
        {
            if (await _usersService.DeleteUser(userId) == EntityState.Deleted)
                return Ok();
            else
                return NoContent();
        }
    }
}
