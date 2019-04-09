using Gaugeter.Api.Devices.Models.Dto;

namespace Gaugeter.Api.Users.Models.Dto
{
    public class UserDeviceDto
    {
        public UserDto User { get; set; }

        public string UserId { get; set; }

        public string BluetoothAddress { get; set; }

        public DeviceDto Device { get; set; }

    }
}
