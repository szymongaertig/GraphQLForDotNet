using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CatteryRegister
{
    public class CatteryAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public static string Schema = "Bearer";

        public CatteryAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        private static Regex _tokenRegex = new Regex("^Bearer (.*)$");

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers
                .FirstOrDefault(x => x.Key == "Authorization");

            if (string.IsNullOrWhiteSpace(token.Value))
                return AuthenticateResult.NoResult();

            var matchResult = _tokenRegex.Match(token.Value);
            if (!matchResult.Success)
                return AuthenticateResult.NoResult();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            };

            switch (matchResult.Groups[1].Value)
            {
                case "owner_of_cattery_1":
                    AddPermission(claims, "owner:1");
                    break;
                case "admin":
                    AddPermission(claims, "admin");
                    break;
            }

            var identity = new ClaimsIdentity(claims, Schema);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Schema);
            var result = AuthenticateResult.Success(ticket);
            return result;
        }

        private static void AddPermission(List<Claim> claims, string value)
        {
            claims.Add(new Claim("permissions", value));
        }
    }
}