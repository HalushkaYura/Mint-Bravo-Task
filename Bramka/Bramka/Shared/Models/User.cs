namespace Bramka.Shared.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsInside { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }





    }
}
