using Bramka.Client.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Bramka.Client.Middlewares
{
    public class AuthMiddleware : IMiddleware
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAuthService _authService;
        private readonly HttpClient _httpClient;

        public AuthMiddleware(IAuthService authService, IHttpContextAccessor httpContextAccessor, HttpClient _httpClient)
        {
            _authService = authService;
            _contextAccessor = httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(await _authService.IsExpiredTokenAsync())
            {
            }

            await next(context);
        }
    }
}
