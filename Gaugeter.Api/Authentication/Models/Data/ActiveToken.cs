using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaugeter.Api.Authentication.Models.Data
{
    public class ActiveToken
    {
        [Key]
        public string Token { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
