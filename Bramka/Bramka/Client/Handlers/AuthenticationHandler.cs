
using Bramka.Client.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace Bramka.Client.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthService _authService;
        private bool _refreshing;

        public AuthenticationHandler(IAuthService authService)
        {
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await _authService.GetJwtAsync();

            if (!string.IsNullOrEmpty(jwt))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Replace("\"", ""));
            
            var response = await base.SendAsync(request, cancellationToken);

            if (!_refreshing && !string.IsNullOrEmpty(jwt) && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                try
                {
                    _refreshing = true;

                    if (await _authService.RefreshTokenAsync())
                    {
                        jwt = await _authService.GetJwtAsync();

                        if (!string.IsNullOrEmpty(jwt))
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt.Replace("\"", ""));

                        response = await base.SendAsync(request, cancellationToken);
                    }
                }
                finally
                {
                    _refreshing = false;
                }
            }

            return response;
        }
    }
}
