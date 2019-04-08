using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaugeter.Api.Authentication.Models
{
    public class ActiveToken
    {
        public ActiveToken() { }

        [Key]
        [Required]
        public string Token { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
