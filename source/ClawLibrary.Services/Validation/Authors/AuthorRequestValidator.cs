using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Authors;
using FluentValidation;

namespace ClawLibrary.Services.Validation.Authors
{
    public class AuthorRequestValidator : AbstractValidator<AuthorRequest>
    {
        public AuthorRequestValidator()
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

            RuleFor(x => x.Description.Trim().Length)
                .LessThan(2000)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("Description");
        }
    }
}
