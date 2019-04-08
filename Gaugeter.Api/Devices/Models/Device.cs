using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gaugeter.Api.Users.Models;

namespace Gaugeter.Api.Devices.Models
{
    public class Device
    {
        public Device() { }

        [Key]
        [Required]
        public string BluetoothAddress { get; set; }

        [Required]
        [MaxLength(20), MinLength(5)]
        public string Name { get; set; }

        public List<UserDevice> DeviceUsers { get; set; }
    }
}
