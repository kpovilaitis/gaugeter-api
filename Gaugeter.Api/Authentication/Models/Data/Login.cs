using Gaugeter.Api.Users.Models.Data;

namespace Gaugeter.Api.Authentication.Models.Data
{
    public class Login
    {
        public User User { get; set; }

        public string Token { get; set; }
    }
}
