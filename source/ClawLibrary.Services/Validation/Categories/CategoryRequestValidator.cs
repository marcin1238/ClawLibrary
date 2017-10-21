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
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Name");

            RuleFor(x => x.Name.Trim().Length)
                .GreaterThan(2)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("Name")
                .LessThan(256)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("Name");
        }
    }
}
