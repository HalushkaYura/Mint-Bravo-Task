namespace Bramka.Shared.DTOs.UserDTO
{
    public class UserRegistrationDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
    }
}
