using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Models.Auth;
using FluentValidation;

namespace ClawLibrary.Services.Validation.Users
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .WithName("Email")
                .EmailAddress()
                .WithMessage(ErrorCode.ValidationEmailFormat.ToString())
                .WithName("Email");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .WithName("Password")
                .Length(8, 100)
                .WithMessage(ErrorCode.ValidationPasswordLength.ToString())
                .WithName("Password");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage(ErrorCode.ValidationPasswordDoesNotMatch.ToString())
                .WithName("ConfirmPassword");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .WithName("FirstName")
                .Length(2, 50)
                .WithMessage(ErrorCode.ValidationStringLength.ToString())
                .WithName("FirstName");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .WithName("LastName")
                .Length(2, 50)
                .WithMessage(ErrorCode.ValidationStringLength.ToString())
                .WithName("LastName");

            RuleFor(x => x.PhoneNumber.Trim())
                .Length(8, 20)
                .WithMessage(ErrorCode.ValidationStringLength.ToString())
                .WithName("PhoneNumber");
        }
    }
}
