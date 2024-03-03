using Bramka.Server.Constans;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using Dapper;
using System.Data;

namespace Bramka.Server.Services
{
    public class LogService : ILogService
    {
        private readonly IDbConnection _connection;
        public LogService(IDbConnection dbConnection) 
        {
            _connection = dbConnection; 
        }
        public async Task<bool> CreateLogAsync(Log newLog)
        {

            var row = await _connection.ExecuteAsync(DataBaseConstants.CreateLog,
                new
                {
                    newLog.ActionType,
                    newLog.Description,
                    newLog.UserId
                }, commandType: CommandType.StoredProcedure);

            return row > 0;
        }

        public async Task<bool> DeleteLogAsync(int id)
        {
            var row = await _connection.ExecuteAsync(DataBaseConstants.DeleteLog,
                new { LogId = id }, commandType: CommandType.StoredProcedure);

            return row > 0;
        }

        public async Task<Log> GeLogByIdAsync(int id)
        {
            var log = await _connection.QueryFirstOrDefaultAsync<Log>(DataBaseConstants.GetLogById,
                                                       new { LogId = id },
                                                       commandType: CommandType.StoredProcedure);
            return log;
        }

        public async Task<IEnumerable<Log>> GetAllLogsAsync()
        {
            return await _connection.QueryAsync<Log>(DataBaseConstants.GetAllLogs);
        }

        public async Task<IEnumerable<Log>> GetLogByUserIdAsync(Guid userId)
        {
            var logs = await _connection.QueryAsync<Log>(DataBaseConstants.GetAllLogByUserId,
                new { UserId = userId }, commandType: CommandType.StoredProcedure);

            return logs;
        }
    }
}
