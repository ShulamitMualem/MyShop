// Middleware to copy JWT token from cookie to Authorization header
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyShop.Middleware
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("jwtToken"))
            {
                var token = context.Request.Cookies["jwtToken"];
                if (!string.IsNullOrEmpty(token) && !context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
            }
            await _next(context);
        }
    }

    public static class JwtCookieMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtCookieMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtCookieMiddleware>();
        }
    }
}
