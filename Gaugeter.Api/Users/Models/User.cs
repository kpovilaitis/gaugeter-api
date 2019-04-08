using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gaugeter.Api.Constants;

namespace Gaugeter.Api.Users.Models
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

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public List<UserDevice> UserDevices { get; set; }
    }
}
