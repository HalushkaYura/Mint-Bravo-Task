namespace Bramka.Client.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> IsExpiredTokenAsync();
    }
}
