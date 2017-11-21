using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Users;
using FluentValidation;

namespace ClawLibrary.Services.Validation.Users
{
    public class SetUserPasswordRequestValidator : AbstractValidator<SetUserPasswordRequest>
    {
        public SetUserPasswordRequestValidator()
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

            RuleFor(x => x.VerificationCode)
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("VerificationCode")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("VerificationCode")
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .WithName("VerificationCode")
                .Length(36, 36)
                .OverridePropertyName("VerificationCode")
                .WithMessage(ErrorCode.InvalidFormat.ToString());
        }
    }
}