using System.Threading.Tasks;
using CarGaugesApi.Authentication.Models;
using CarGaugesApi.Authentication.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGaugesApi.Authentication.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto login)
        {
            var user = await _authService.Authenticate(login.UserId, login.Password);

            if (user == null)
                return Unauthorized("Invalid login credentials");
                
            return Ok(user);
        }
    }
}
