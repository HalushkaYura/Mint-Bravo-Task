using Bramka.Shared.DTOs.QrCodeDTO;
using Bramka.Shared.Models;

namespace Bramka.Shared.Interfaces.Services
{
    public interface IQrCodeService
    {
        Task<bool> DeleteQrCodeAsync(int id);
        Task<IEnumerable<QrCode>> GetAllQrCodesAsync();
        Task<QrCode?> GetQrCodeByIdAsync(int? id);
        Task <IEnumerable<QrCode>> GetQrCodeByUserIdAsync(Guid userId);
        Task<int> CreateQrCodeAsync(QrCodeCreateDTO newQrCode);
        Task<bool> UpdateQrCodeAsync(QrCode newQrCode, int id);
        Task<QrCode?> GetLastQrCodeAsync(Guid? id);
        Task<bool> UseQrCodeAsync(int id);



    }
}
