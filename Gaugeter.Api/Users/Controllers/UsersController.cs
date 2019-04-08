using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Gaugeter.Api.Users.Services;
using Gaugeter.Api.Users.Models;

namespace Gaugeter.Api.Users.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        public readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _usersService.GetUser(userId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersService.GetAllUsers();

            if (users == null)
                return NotFound();

            return Ok(users);
        }

        // GET api/users
        [HttpGet("Values")]
        public async Task<IActionResult> GetValues()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // POST api/users
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]User user)
        {
            if (user == null)
                return BadRequest(ModelState);

            var state = await _usersService.CreateUser(user);

            if (state == EntityState.Added)
                return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // PUT api/users/5
        [HttpPut]
        public async Task<IActionResult> UpdateUser ([FromBody] User user)
        {
            if (user == null)
                return BadRequest(ModelState);

            if (await _usersService.UpdateUser(user) == EntityState.Modified)
                return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            else
                return BadRequest(ModelState);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            if (await _usersService.DeleteUser(userId) == EntityState.Deleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
