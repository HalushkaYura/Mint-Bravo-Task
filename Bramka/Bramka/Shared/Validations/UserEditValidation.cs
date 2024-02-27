using Bramka.Server.Interfaces;
using Bramka.Shared.DTOs.UserDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bramka.Shared.Validations
{
    public class UserEditValidation : AbstractValidator<UserEditDTO>
    {
        private readonly IUserService _userService;
        public UserEditValidation(IUserService userService) 
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

            RuleFor(user => user.PhoneNumber)
                .NotEmpty().When(user => !string.IsNullOrWhiteSpace(user.PhoneNumber))
                    .WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$").When(user => !string.IsNullOrWhiteSpace(user.PhoneNumber))
                    .WithMessage("Phone number must consist of exactly 10 digits.");

            RuleFor(user => user.RoleId)
                .NotNull()
                .GreaterThan(0)
                .LessThan(3)
                .WithMessage("Role must be selected.");
        }

        private bool IsUniqueUserEmail(string email)
        {
            return !_userService.IsEmailExist(email);
        }
    }
}
