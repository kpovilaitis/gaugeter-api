using System.ComponentModel.DataAnnotations;

namespace Gaugeter.Api.Devices.Models.Data
{
    public class Device
    {
        [Key]
        [Required]
        [StringLength(18)]
        public string BluetoothAddress { get; set; }

        [Required]
        [MaxLength(20), MinLength(5)]
        public string Name { get; set; }
    }
}
