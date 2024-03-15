using Bramka.Shared.DTOs.UserDTO;
using FluentValidation;

namespace Bramka.Shared.Validations
{
    public class UserSetPasswordValidation : AbstractValidator<UserSetPasswordDTO>
    {
        public UserSetPasswordValidation()
        {

            RuleFor(user => user.NewPassword)

                .NotEmpty()
                .MinimumLength(6)
                .Matches("[A-Z]").WithMessage("{PropertyName} must contain one or more capital letters.")
                .Matches("[a-z]").WithMessage("{PropertyName} must contain one or more lowercase letters.")
                .Matches(@"\d").WithMessage("{PropertyName} must contain one or more digits.")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("{PropertyName} must contain one or more special characters.")
                .Matches("^[^£# “”]*$").WithMessage("{PropertyName} must not contain the following characters £ # “” or spaces.");

        }
    }
}
