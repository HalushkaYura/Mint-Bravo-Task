using Blazored.LocalStorage;
using Bramka.Client.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Bramka.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;

        public AuthService(ILocalStorageService localStorageService)
        {
            _localStorage = localStorageService;
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

        public void GetJWTToken()
        {

        }
    }
}
