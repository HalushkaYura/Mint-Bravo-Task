using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bramka.Shared.DTOs.UserDTO
{
    public class UserEditDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? PhoneNumber { get; set; }
        //public string Password { get; set; }
        //public string Email { get; set; }
        public int RoleId { get; set; }

    }
}
