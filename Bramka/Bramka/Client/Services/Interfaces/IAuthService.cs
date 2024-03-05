namespace Bramka.Client.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> IsExpiredTokenAsync();
        Task<string?> GetJwtAsync();
        Task<bool> RefreshTokenAsync();
    }
}
