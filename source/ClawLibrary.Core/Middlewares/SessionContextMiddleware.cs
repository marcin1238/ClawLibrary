using System.Security.Claims;
using System.Threading.Tasks;
using ClawLibrary.Core.Services;
using Microsoft.AspNetCore.Http;

namespace ClawLibrary.Core.Middlewares
{
    public class SessionContextMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                PerRequestSessionContextProvider.SetContext(
                    new SessionContext
                    {
                        UserId = SafeParseToLong(context.User.FindFirst(ClaimTypes.Sid)?.Value),
                        UserEmail = context.User.FindFirst(ClaimTypes.Name)?.Value
                    }
                );
            }

            await _next.Invoke(context);
        }

        private long SafeParseToLong(string stringValue)
        {
            if (long.TryParse(stringValue, out long longValue))
            {
                return longValue;
            }
            return 0;
        }
    }
}
