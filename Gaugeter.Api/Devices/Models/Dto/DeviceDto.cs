using System.ComponentModel.DataAnnotations;

namespace Gaugeter.Api.Devices.Models.Dto
{
    public class DeviceDto
    {
        [Required]
        public string BluetoothAddress { get; set; }

        [Required]
        [MaxLength(20), MinLength(5)]
        public string Name { get; set; }
    }
}
