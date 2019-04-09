using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gaugeter.Api.Users.Models.Data;

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

        public IList<UserDevice> Users { get; set; }
    }
}
