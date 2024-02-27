using Bramka.Shared.Models;

namespace Bramka.Shared.Interfaces.Services
{
    public interface ILogService
    {
        Task<IEnumerable<Log>> GetAllLogsAsync();
        Task<IEnumerable<Log>> GetLogByUserIdAsync(Guid userId);
        Task<Log> GeLogByIdAsync(int id);
        Task<bool> CreateLogAsync(Log newItem);
        Task<bool> DeleteLogAsync(int id);
    }
}
