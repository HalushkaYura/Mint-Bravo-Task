namespace Bramka.Shared.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public string ActionType { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UserId { get; set; }

    }
}
