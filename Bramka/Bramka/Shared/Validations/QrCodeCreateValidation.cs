using Bramka.Shared.DTOs.QrCodeDTO;
using Bramka.Shared.Helpers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bramka.Shared.Validations
{
    public class QrCodeCreateValidation : AbstractValidator<QrCodeCreateDTO>
    {
        public QrCodeCreateValidation()
        {
            RuleFor(code => code.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid user");

            RuleFor(code => code.CodeHash)
                .Must(codeHash => Enum.TryParse(typeof(QrCodeKey), codeHash, out _))
                .WithMessage("Invalid QR code key.");
        }
    }

    
}
