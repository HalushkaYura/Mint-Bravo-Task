namespace Bramka.Shared.Models
{
    public class QrCode
    {
        public int QrCodeId { get; set; }
        public string CodeHash { get; set; }
        public int GenerationCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }
}
