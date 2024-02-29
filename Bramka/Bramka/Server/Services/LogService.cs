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
            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var row = await connection.ExecuteAsync(DataBaseConstants.CreateLog,
                new
                {
                    newLog.ActionType,
                    newLog.Description,
                    newLog.QrCodeId,
                    newLog.UserId
                }, commandType: CommandType.StoredProcedure);

            return row > 0;
        }

        public async Task<bool> DeleteLogAsync(int id)
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var row = await connection.ExecuteAsync(DataBaseConstants.DeleteLog,
                new { LogId = id }, commandType: CommandType.StoredProcedure);

            return row > 0;
        }

        public async Task<Log> GeLogByIdAsync(int id)
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var log = await connection.QueryFirstOrDefaultAsync<Log>(DataBaseConstants.GetLogById,
                                                       new { LogId = id },
                                                       commandType: CommandType.StoredProcedure);
            return log;
        }

        public async Task<IEnumerable<Log>> GetAllLogsAsync()
        {
            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            return await connection.QueryAsync<Log>(DataBaseConstants.GetAllLogs);
        }

        public async Task<IEnumerable<Log>> GetLogByUserIdAsync(Guid userId)
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var logs = await connection.QueryAsync<Log>(DataBaseConstants.GetAllLogByUserId,
                new { UserId = userId }, commandType: CommandType.StoredProcedure);

            return logs;
        }
    }
}
