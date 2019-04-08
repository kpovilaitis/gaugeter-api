using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Gaugeter.Api.Authentication.Models.Data;
using Gaugeter.Api.Authentication.Models.Dto;
using Gaugeter.Api.Authentication.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gaugeter.Api.Authentication.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody][Required] LoginDto login)
        {
            var loginData = await _authService.Authenticate(login.User.UserId, login.User.Password);

            if (loginData == null)
                return Unauthorized("Invalid login credentials");
                
            return Ok(_mapper.Map<Login, LoginDto>(loginData));
        }
    }
}
