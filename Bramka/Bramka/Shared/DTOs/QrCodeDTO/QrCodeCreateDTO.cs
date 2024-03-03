namespace Bramka.Shared.DTOs.QrCodeDTO
{
    public class QrCodeCreateDTO
    {
        public Guid UserId { get; set; }
        public string CodeHash { get; set; }

    }
}
