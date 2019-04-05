using System;
using System.Linq;
using CarGaugesApi.Authentication.Configuration;
using Microsoft.AspNetCore.Http;

namespace CarGaugesApi.Authentication.Services.UserInfoAccessor
{
    public class UserInfoAccessor : IUserInfoAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfoAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                throw new InvalidOperationException($"Unable to extract {nameof(GaugeterClaimTypes.UserId)} from unauthenticated context.");

            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == GaugeterClaimTypes.UserId)?.Value;

            if (userId == null)
                throw new InvalidOperationException($"Unable to extract {nameof(GaugeterClaimTypes.UserId)} from unauthenticated context.");

            return userId;
        }
    }
}
