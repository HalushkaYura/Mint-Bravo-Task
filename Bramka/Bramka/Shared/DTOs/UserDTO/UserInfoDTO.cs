namespace Bramka.Shared.DTOs.UserDTO
{
    public class UserInfoDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate {  get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsInside { get; set; }
        public int RoleId { get; set; }
    }
}
