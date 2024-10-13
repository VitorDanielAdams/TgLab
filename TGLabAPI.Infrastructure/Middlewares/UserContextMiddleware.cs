using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TGLabAPI.Application.DTOs.Auth;

namespace TGLabAPI.Infrastructure.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserContext userContext)
        {
            if (context.User.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                var emailClaim = identity.FindFirst(ClaimTypes.Email);

                if (userIdClaim != null)
                {
                    userContext.Id = Guid.Parse(userIdClaim.Value);
                }

                if (emailClaim != null)
                {
                    userContext.Email = emailClaim.Value;
                }
            }

            await _next(context);
        }
    }
}
