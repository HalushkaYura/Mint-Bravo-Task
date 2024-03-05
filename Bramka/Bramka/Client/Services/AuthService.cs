using Blazored.LocalStorage;
using Bramka.Client.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace Bramka.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public AuthService(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorage = localStorageService;
            _httpClient = httpClient;
        }

        public async Task<bool> IsExpiredTokenAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync("token");
            token = token?.Replace("\"", "");


            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(token))
                return true;

            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                return true;
            }

            var expires = jwtToken.ValidTo;
            var now = DateTime.UtcNow;

            return now > expires;
        }

        public async Task<string?> GetJwtAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync("token");
            return token;
        }

        public async Task<bool> RefreshTokenAsync()
        {
            var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "api/Auth/refresh-token", null);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();

                await _localStorage.SetItemAsync("token", token);
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _localStorage.RemoveItemAsync("token");
                return false;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("token");
        }
    }
}
