using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Categories;
using FluentValidation;

namespace ClawLibrary.Services.Validation.Categories
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(x => x.Name.Trim())
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Name")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Name");

            RuleFor(x => x.Name.Trim().Length)
                .GreaterThan(2)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("Name")
                .LessThan(100)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("Name");
        }
    }
}
