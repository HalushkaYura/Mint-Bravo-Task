using Bramka.Server.Interfaces;
using Bramka.Shared.DTOs.UserDTO;
using FluentValidation;

namespace Bramka.Shared.Validations
{
    public class UserRegistrationValidation : AbstractValidator<UserRegistrationDTO>
    {
        private readonly IUserService _userService;
        public UserRegistrationValidation(IUserService userService)
        {
            _userService = userService;

            RuleFor(user => user.Name)
              .NotNull()
              .Length(3, 50);


            RuleFor(user => user.Surname)
                .NotNull()
                .Length(3, 50);


            RuleFor(user => user.Email)
                .NotNull()
                .EmailAddress()
                .Must(IsUniqueUserEmail)
                .WithMessage("{PropertyName} already exists.");

            RuleFor(user => user.Password)
                .NotEmpty()
                .MinimumLength(6)
                .Matches("[A-Z]").WithMessage("{PropertyName} must contain one or more capital letters.")
                .Matches("[a-z]").WithMessage("{PropertyName} must contain one or more lowercase letters.")
                .Matches(@"\d").WithMessage("{PropertyName} must contain one or more digits.")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("{PropertyName} must contain one or more special characters.")
                .Matches("^[^£# “”]*$").WithMessage("{PropertyName} must not contain the following characters £ # “” or spaces.");

            RuleFor(user => user.PhoneNumber)
                .NotEmpty().When(user => !string.IsNullOrWhiteSpace(user.PhoneNumber)) 
                    .WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$").When(user => !string.IsNullOrWhiteSpace(user.PhoneNumber))
                    .WithMessage("Phone number must consist of exactly 10 digits.");

        }
        private bool IsUniqueUserEmail(string email)
        {
            return !_userService.IsEmailExist(email);
        }
    }
}
