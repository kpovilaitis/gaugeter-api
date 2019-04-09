using Gaugeter.Api.Devices.Models.Data;

namespace Gaugeter.Api.Users.Models.Data
{
    public class UserDevice
    {
        public User User { get; set; }

        public string UserId { get; set; }

        public string BluetoothAddress { get; set; }

        public Device Device { get; set; }
    }
}
