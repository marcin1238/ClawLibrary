using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Users;
using FluentValidation;

namespace ClawLibrary.Services.Validation.Users
{
    
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
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
