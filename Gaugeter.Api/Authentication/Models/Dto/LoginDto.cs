    using Gaugeter.Api.Users.Models.Dto;

namespace Gaugeter.Api.Authentication.Models.Dto
{
    public class LoginDto
    {
        public UserDto User { get; set; }

        public string Token { get; set; }
    }
}
