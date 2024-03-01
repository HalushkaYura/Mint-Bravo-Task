﻿namespace Bramka.Shared.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsInside { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }





    }
}
