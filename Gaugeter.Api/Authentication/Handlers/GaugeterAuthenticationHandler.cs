using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Gaugeter.Api.Authentication.Configuration;
using Gaugeter.Api.Authentication.Services.TokenService;
using Gaugeter.Api.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Gaugeter.Api.Authentication.Handlers
{
    public class GaugeterAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ITokenService _tokenService;
        private readonly AuthenticationSettings _apiSettings;

        public GaugeterAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITokenService tokenService,
            IOptionsSnapshot<AuthenticationSettings> apiSettings
        )
            : base(options, logger, encoder, clock)
        {
            _tokenService = tokenService;
            _apiSettings = apiSettings.Value;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var bearer = Request.Headers.SingleOrDefault(header => header.Key == "Authorization");
            var bearerToken = bearer.Value;
            
            if (string.IsNullOrEmpty(bearerToken))
                return AuthenticateResult.Fail("Unauthorized");

            var token = await _tokenService.GetToken(bearerToken);
            
            if (token == null)
                return AuthenticateResult.Fail("Token not found");

            var tokenValidForHours = (token.ExpirationDate - DateTime.Now).TotalHours;
            
            if (tokenValidForHours < 0)
            {
                await _tokenService.RemoveToken(token.Token);
                return AuthenticateResult.Fail("Token expired");
            }

            if (tokenValidForHours < _apiSettings.TokenValidHours / 2)
                await _tokenService.UpdateTokenExpiration(token.Token, DateTime.Now.AddHours(_apiSettings.TokenValidHours));

            var claims = new List<Claim> { new Claim(GaugeterClaimTypes.UserId, token.UserId) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

    }
}
