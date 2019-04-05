using System.Threading.Tasks;
using CarGaugesApi.Authentication.Services.UserInfoAccessor;
using CarGaugesApi.Models;
using CarGaugesApi.Services.Devices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarGaugesApi.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : Controller
    {
        public readonly IDevicesService _devicesService;
        public readonly IUserInfoAccessor _userInfoAccessor;

        public DevicesController(IDevicesService devicesService, IUserInfoAccessor userInfoAccessor)
        {
            _devicesService = devicesService;
            _userInfoAccessor = userInfoAccessor;
        }

        // POST api/users
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddDeviceToUser([FromBody]Device device)
        {
            var state = await _devicesService.AddDeviceToUser(_userInfoAccessor.GetUserId(), device);

            return Ok();
        }
    }
}
