namespace Bramka.Shared.Models
{
    public class JWTToken 
    {
        public int TokenId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }
    }
}
