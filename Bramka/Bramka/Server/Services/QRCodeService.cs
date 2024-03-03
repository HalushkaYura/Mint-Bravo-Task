using Bramka.Server.Constans;
using Bramka.Shared.DTOs.QrCodeDTO;
using Bramka.Shared.Helpers;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;

namespace Bramka.Server.Services
{
    public class QrCodeService : IQrCodeService
    {
        private readonly IDbConnection _connection;
        public QrCodeService(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }
        public async Task<int> CreateQrCodeGuestAsync(string code)
        {
            if(!Enum.IsDefined(typeof(QrCodeKey), code))
            {
                throw new InvalidOperationException("Invalid QR code key.");
            }
            if (await CheckUserHasKeyAsync((Guid?)null, code))
            {
                throw new InvalidOperationException("QR code already exist.");
            }

            var parameters = new DynamicParameters(
             new
             {
                 CodeHash = code,
                 UserId = (Guid ?)null
             });
            parameters.Add("@QrCodeId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _connection.ExecuteAsync(DataBaseConstants.CreateQrCode,
                                            parameters,
                                            commandType: CommandType.StoredProcedure);
            return parameters.Get<int>("@QrCodeId");
        }

        public async Task<int> CreateQrCodeAsync(QrCodeCreateDTO newQrCode)
        {
            if (await CheckUserExistAsync(newQrCode.UserId))
            {
                throw new InvalidOperationException("User does not exist");
            }

            if (await CheckUserHasKeyAsync(newQrCode.UserId, newQrCode.CodeHash))
            {
                throw new InvalidOperationException("QR code already exist.");
            }

            var parameters = new DynamicParameters(
             new
             {
                 newQrCode.CodeHash,
                 newQrCode.UserId
             });
            parameters.Add("@QrCodeId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _connection.ExecuteAsync(DataBaseConstants.CreateQrCode,
                                            parameters,
                                            commandType: CommandType.StoredProcedure);
            return parameters.Get<int>("@QrCodeId");
        }


        private async Task<bool> CheckUserExistAsync(Guid userId)
        {
            var  user = await _connection.QueryFirstOrDefaultAsync(DataBaseConstants.GetUserById,
                        new { UserId = userId},
                        commandType: CommandType.StoredProcedure);

            return user == null;
        }

        private async Task<bool> CheckUserHasKeyAsync(Guid? userId, string code)
        {
            var obj = await _connection.QueryFirstOrDefaultAsync(DataBaseConstants.QrCodeExist,
                        new { UserId = userId, Code = code },
                        commandType: CommandType.StoredProcedure);

            return obj != null;
        }

        public async Task<bool> DeleteQrCodeAsync(int qrCodeId)
        {
            var row = await _connection.ExecuteAsync(DataBaseConstants.DeleteQrCode,
                new { QrCodeId = qrCodeId }, commandType: CommandType.StoredProcedure);

            return row > 0;
        }

        public async Task<IEnumerable<QrCode>> GetAllQrCodesAsync()
        {

            return await _connection.QueryAsync<QrCode>(DataBaseConstants.GetAllQrCodes);
        }

        public async Task<QrCode?> GetLastQrCodeAsync(Guid userId)
        {
            if (await CheckUserExistAsync(userId))
            {
                throw new InvalidOperationException("User does not exist.");

            }

            var qrCode = await _connection.QueryFirstOrDefaultAsync<QrCode>(
                DataBaseConstants.GetLastQrCode,
                new
                {
                    UserId = userId
                }, commandType: CommandType.StoredProcedure);

            return qrCode;
        }

        public async Task<QrCode?> GetQrCodeByIdAsync(int? qrCodeId)
        {

            var qrCode = await _connection.QueryFirstOrDefaultAsync<QrCode>(DataBaseConstants.GetQrCodeById,
                                                        new { QrCodeId = qrCodeId },
                                                        commandType: CommandType.StoredProcedure);
            return qrCode;
        }

        public async Task<IEnumerable<QrCode>> GetQrCodeByUserIdAsync(Guid userId)
        {
            if (await CheckUserExistAsync(userId))
            {
                throw new InvalidOperationException("User does not exist.");

            }
            var QrCode = await _connection.QueryAsync<QrCode>(DataBaseConstants.GetAllQrCodeByUserId,
                new { UserId = userId }, commandType: CommandType.StoredProcedure);

            return QrCode;
        }

        public async Task<bool> UpdateQrCodeAsync(QrCode newQrCode, int id)
        {
            var row = await _connection.ExecuteAsync(DataBaseConstants.UpdateQrCode,
                new
                {
                    QrCodeId = id,
                    newQrCode.CodeHash,
                    newQrCode.GenerationCount,
                    newQrCode.UserId
                }, commandType: CommandType.StoredProcedure);

            return row > 0;
        }

    }
}
