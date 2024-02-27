using Bramka.Server.Constans;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using Dapper;
using System.Data;

namespace Bramka.Server.Services
{
    public class LogService : ILogService
    {
        public async Task<bool> CreateLogAsync(Log newLog)
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                var row = await connection.ExecuteAsync(DataBaseConstants.CreateLog,
                    new
                    {
                        newLog.ActionType,
                        newLog.Description,
                        CreatedAt = DateTime.Now,
                        newLog.QrCodeId,
                        newLog.UserId
                    }, commandType: CommandType.StoredProcedure);

                return row > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error logging action: {ex.Message}");
            }
        }

        public async Task<bool> DeleteLogAsync(int id)
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                var row = await connection.ExecuteAsync(DataBaseConstants.DeleteLog,
                    new { LogId = id }, commandType: CommandType.StoredProcedure);

                return row > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting log: {ex.Message}");
            }
        }

        public async Task<Log> GeLogByIdAsync(int id)
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                 var log = await connection.QueryFirstOrDefaultAsync<Log>(DataBaseConstants.GetLogById,
                                                            new { LogId = id },
                                                            commandType: CommandType.StoredProcedure);
                return log;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error  not found log for id: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Log>> GetAllLogsAsync()
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                return await connection.QueryAsync<Log>(DataBaseConstants.GetAllLogs);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all users: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Log>> GetLogByUserIdAsync(Guid userId)
        {
            try
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();

                var logs = await connection.QueryAsync<Log>(DataBaseConstants.GetAllLogByUserId,
                    new { UserId = userId }, commandType: CommandType.StoredProcedure);

                return logs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting QR code by user: {ex.Message}");
            }
        }
    }
}
