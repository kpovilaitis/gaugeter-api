using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gaugeter.Api.Constants;
using Gaugeter.Api.Devices.Models.Data;

namespace Gaugeter.Api.Users.Models.Data
{
    public class User
    {
        [Key]
        [Required]
        [MaxLength(20), MinLength(5)]
        public string UserId { get; set; }

        [Required]
        public string Password { get; set; }

        public string Description { get; set; }

        [Required]
        public Enums.MEASUREMENT_SYSTEM MeasurementSystem { get; set; }

        public IList<Device> Devices { get; set; }
    }
}
