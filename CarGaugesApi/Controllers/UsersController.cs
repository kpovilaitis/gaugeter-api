using System.Threading.Tasks;
using CarGaugesApi.Models;
using CarGaugesApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using CarGaugesApi.Constants;

namespace CarGaugesApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public readonly IUsersService _usersService;
        public IHttpContextAccessor _httpContextAccessor;

        public UsersController(IUsersService usersService, IHttpContextAccessor httpContextAccessor)
        {
            _usersService = usersService;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            if (double.Parse(_httpContextAccessor.HttpContext.Request.Headers["AppVersion"]) == AppVersions.APP_VERSION_APPLE)
            {
                var user = _usersService.Authenticate(userParam.Username, userParam.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(user);
            }

            return BadRequest();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _usersService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET api/users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _usersService.GetAllUsers();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // POST api/users
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var state = _usersService.CreateUser(user);

            if (state == EntityState.Added)
            {
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/users/5
        [HttpPut]
        public IActionResult UpdateUser ([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
            {
                return BadRequest(ModelState);
            }

            if (_usersService.UpdateUser(user) == EntityState.Modified)
            {
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == -1)
            {
                return BadRequest(ModelState);
            }

            if (_usersService.DeleteUser(id) == EntityState.Deleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
