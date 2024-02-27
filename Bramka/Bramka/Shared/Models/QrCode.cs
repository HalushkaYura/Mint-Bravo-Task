namespace Bramka.Shared.Models
{
    public class QrCode
    {
        public int QrCodeId { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime UseDate { get; set; }
        public Guid UserId { get; set; }
    }
}
