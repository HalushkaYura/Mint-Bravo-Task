using Bramka.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bramka.Shared.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendConfirmEmailAsync(User user);
        Task<int> ConfirmEmailAsync(string code);
        Task SendResetPasswordEmailAsync(User user);
    }
}
