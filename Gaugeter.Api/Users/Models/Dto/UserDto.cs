using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gaugeter.Api.Constants;
using Gaugeter.Api.Devices.Models.Dto;

namespace Gaugeter.Api.Users.Models.Dto
{
    public class UserDto
    {
        [Required]
        [MaxLength(20), MinLength(5)]
        public string UserId { get; set; }

        [Required]
        public string Password { get; set; }

        public string Description { get; set; }

        [Required]
        public Enums.MEASUREMENT_SYSTEM MeasurementSystem { get; set; }

        //public IList<DeviceDto> Devices { get; set; }
    }
}
