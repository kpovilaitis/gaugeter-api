using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Gaugeter.Api.Authentication.Services.UserInfoAccessor;
using Gaugeter.Api.Devices.Models.Data;
using Gaugeter.Api.Devices.Models.Dto;
using Gaugeter.Api.Services.Devices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Devices.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DevicesController : Controller
    {
        private readonly IDevicesService _devicesService;
        private readonly IMapper _mapper;
        private readonly IUserInfoAccessorService _userInfoAccessor;

        public DevicesController(IDevicesService devicesService, IMapper mapper, IUserInfoAccessorService userInfoAccessor)
        {
            _devicesService = devicesService;
            _mapper = mapper;
            _userInfoAccessor = userInfoAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> AddDeviceToUser([FromBody][Required] Device device)
        {
            if (EntityState.Added == await _devicesService.AddDeviceToUser(_userInfoAccessor.GetUserId(), device)) 
            {
                var devices = await _devicesService.GetUserDevices(_userInfoAccessor.GetUserId());

                var mappedDevices = _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceDto>>(devices);

                if (mappedDevices == null)
                    return NoContent();

                return Ok(mappedDevices);
            } 
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody][Required] Device device)
        {
            if (EntityState.Added == await _devicesService.Create(device))
            {
                var mappedDevice = _mapper.Map<Device, DeviceDto>(device);
                return CreatedAtAction(nameof(Get), new { id = device.BluetoothAddress }, mappedDevice);
            }
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody][Required] string bluetoothAddress)
        {
            var device = await _devicesService.Get(bluetoothAddress);

            if (device == null)
                return NoContent();

            return Ok(device);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDevices()
        {
            var devices = await _devicesService.GetUserDevices(_userInfoAccessor.GetUserId());

            var mappedDevices = _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceDto>>(devices);

            if (mappedDevices == null)
                return NoContent();

            return Ok(mappedDevices);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery][Required] string bluetoothAddress)
        {
            if (await _devicesService.Remove(_userInfoAccessor.GetUserId(), bluetoothAddress) == EntityState.Deleted)
            {
                var devices = await _devicesService.GetUserDevices(_userInfoAccessor.GetUserId());

                var mappedDevices = _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceDto>>(devices);

                if (mappedDevices == null)
                    return NoContent();

                return Ok(mappedDevices);
            }
            else
                return NoContent();
        }
    }
}
