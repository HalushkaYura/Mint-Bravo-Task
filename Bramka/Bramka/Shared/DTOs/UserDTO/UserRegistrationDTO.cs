using System.ComponentModel.DataAnnotations;

namespace Bramka.Shared.DTOs.UserDTO
{
    public class UserRegistrationDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
