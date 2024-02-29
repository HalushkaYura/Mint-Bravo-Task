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

        public async Task<int> CreateQrCodeAsync(QrCodeCreateDTO newQrCode)
        {
            if (!Enum.IsDefined(typeof(QrCodeTypes), newQrCode.Type))
            {
                throw new ArgumentException("Invalid QR code type.");
            }

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();
            //Перевірка і де активування старого qr-code
            await DeactiveQrCode(newQrCode.UserId);

            var parameters = new DynamicParameters(
            new
            {
                newQrCode.Type,
                Code = Guid.NewGuid().ToString(),
                ExpirationDate = DateTime.Now.AddMinutes(30),
                newQrCode.UserId
            });
            parameters.Add("@IdQrCode", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(DataBaseConstants.CreateQrCode,
                                            parameters,
                                            commandType: CommandType.StoredProcedure);
            return parameters.Get<int>("@IdQrCode");
        }

        private async Task DeactiveQrCode(Guid userId)
        {
            var activeCode = await GetLastQrCodeAsync(userId);
            if (activeCode != null && activeCode.ExpirationDate > DateTime.Now)
            {
                using var connection = DataBaseConstants.GetConnection();
                await connection.OpenAsync();
                var row = await connection.ExecuteAsync(DataBaseConstants.UpdateQrCode,
                        new
                        {
                            activeCode.Type,
                            activeCode.Code,
                            activeCode.UserId,
                            ExpirationDate = DateTime.Now,
                            QrCodeId = activeCode.QrCodeId
                        }, commandType: CommandType.StoredProcedure);

            }
        }

        public async Task<bool> DeleteQrCodeAsync(int qrCodeId)
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var row = await connection.ExecuteAsync(DataBaseConstants.DeleteQrCode,
                new { QrCodeId = qrCodeId }, commandType: CommandType.StoredProcedure);

            return row > 0;
        }

        public async Task<IEnumerable<QrCode>> GetAllQrCodesAsync()
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            return await connection.QueryAsync<QrCode>(DataBaseConstants.GetAllQrCodes);
        }

        public async Task<QrCode?> GetLastQrCodeAsync(Guid? userId)
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var qrCode = await connection.QueryFirstOrDefaultAsync<QrCode>(
                DataBaseConstants.GetLastQrCode,
                new
                {
                    UserId = userId
                }, commandType: CommandType.StoredProcedure);

            return qrCode;
        }

        public async Task<QrCode?> GetQrCodeByIdAsync(int? qrCodeId)
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var qrCode = await connection.QueryFirstOrDefaultAsync<QrCode>(DataBaseConstants.GetQrCodeById,
                                                        new { QrCodeId = qrCodeId },
                                                        commandType: CommandType.StoredProcedure);

            return qrCode;
        }

        public async Task<IEnumerable<QrCode>> GetQrCodeByUserIdAsync(Guid userId)
        {

            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var QrCode = await connection.QueryAsync<QrCode>(DataBaseConstants.GetAllQrCodeByUserId,
                new { UserId = userId }, commandType: CommandType.StoredProcedure);

            return QrCode;
        }

        public async Task<bool> UpdateQrCodeAsync(QrCode newQrCode, int id)
        {
            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var row = await connection.ExecuteAsync(DataBaseConstants.UpdateQrCode,
                new
                {
                    QrCodeId = id,
                    newQrCode.UseDate,
                    newQrCode.Type,
                    newQrCode.Code,
                    newQrCode.UserId
                }, commandType: CommandType.StoredProcedure);

            return row > 0;
        }

        public async Task<bool> UseQrCodeAsync(int id)
        {
            if (await IsActive(id) == false)
            {
                throw new InvalidOperationException("QR code has expired");
            }
            using var connection = DataBaseConstants.GetConnection();
            await connection.OpenAsync();

            var row = await connection.ExecuteAsync(DataBaseConstants.UpdateQrCodeUseDate,
                new
                {
                    UseDate = DateTime.Now,
                    QrCodeId = id
                },
                commandType: CommandType.StoredProcedure);

            return row > 0;
        }

        private async Task<bool> IsActive(int id)
        {
            var qr = await GetQrCodeByIdAsync(id);
            if (qr != null && qr.ExpirationDate > DateTime.Now && qr.UseDate == DateTime.MinValue)
            {
                return true;
            }
            return false;
        }
    }
}
