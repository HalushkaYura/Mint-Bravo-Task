using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bramka.Shared.DTOs.UserDTO
{
    public class ResetPasswordDTO
    {
        public string Password { get; set; } = string.Empty;
        public string RepeatPassword { get; set; } = string.Empty;
    }
}
