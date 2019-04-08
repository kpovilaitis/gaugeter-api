using System;
using System.ComponentModel.DataAnnotations;

namespace Gaugeter.Api.Authentication.Models.Dto
{
    public class ActiveTokenDto
    {
        [Required]
        public string Token { get; set; }

        public string UserId { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
