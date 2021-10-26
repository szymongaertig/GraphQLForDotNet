using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CatteryRegister
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthMiddleware(RequestDelegate next) => this._next = next;

        public async Task InvokeAsync(
            HttpContext context,
            UserContext userContext)
        {
            if (context.User != null && context.User.Claims.Any())
            {
                IEnumerable<Claim> claims = context.User.Claims;
                var permissionsClaims = claims.Where(c =>
                    c.Type == "permissions");
                if (permissionsClaims.Any())
                {
                    userContext.Permissions = permissionsClaims.Select(x => x.Value.ToString()).ToArray();
                }
            }

            await _next.Invoke(context);
        }
    }
}