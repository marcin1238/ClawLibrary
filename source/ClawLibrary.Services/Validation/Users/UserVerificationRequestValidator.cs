using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Users;
using FluentValidation;

namespace ClawLibrary.Services.Validation.Users
{
    public class UserVerificationRequestValidator : AbstractValidator<UserVerificationRequest>
    {
        public UserVerificationRequestValidator()
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