using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Models.Auth;
using FluentValidation;
using RegisterUserRequest = ClawLibrary.Services.Models.Users.RegisterUserRequest;

namespace ClawLibrary.Services.Validation.Users
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Email")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Email")
                .EmailAddress()
                .WithMessage(ErrorCode.ValidationEmailFormat.ToString())
                .WithName("Email");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Password")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Password")
                .Length(8, 100)
                .WithMessage(ErrorCode.ValidationPasswordLength.ToString())
                .WithName("Password");

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("ConfirmPassword")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("ConfirmPassword")
                .Equal(x => x.Password)
                .WithMessage(ErrorCode.ValidationPasswordDoesNotMatch.ToString())
                .WithName("ConfirmPassword");

            RuleFor(x => x.FirstName.Trim())
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("FirstName")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("FirstName");

            RuleFor(x => x.FirstName.Trim().Length)
                .GreaterThan(3)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("FirstName")
                .LessThan(50)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("FirstName");

            RuleFor(x => x.LastName.Trim())
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("LastName")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("LastName");

            RuleFor(x => x.LastName.Trim().Length)
                .GreaterThan(3)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("LastName")
                .LessThan(50)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("LastName");

        }
    }
}
