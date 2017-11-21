using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Users;
using FluentValidation;

namespace ClawLibrary.Services.Validation.Users
{
    public class ResetUserPasswordRequestValidator : AbstractValidator<ResetUserPasswordRequest>
    {
        public ResetUserPasswordRequestValidator()
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
        }
    }
}