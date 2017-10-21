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
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("FirstName");

            RuleFor(x => x.FirstName.Trim().Length)
                .GreaterThan(1)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("FirstName")
                .LessThan(50)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("FirstName");

            RuleFor(x => x.LastName.Trim())
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("LastName");

            RuleFor(x => x.LastName.Trim().Length)
                .GreaterThan(1)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("LastName")
                .LessThan(50)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("LastName");

           RuleFor(x => x.PhoneNumber.Trim().Length)
                .GreaterThan(7)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("PhoneNumber")
                .LessThan(20)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("PhoneNumber");
        }
    }
}
