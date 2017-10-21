using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Books;
using FluentValidation;

namespace ClawLibrary.Services.Validation.Books
{
    public class BookRequestValidator : AbstractValidator<BookRequest>
    {
        public BookRequestValidator()
        {
            RuleFor(x => x.Title.Trim())
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Title");

            RuleFor(x => x.Title.Trim().Length)
                .GreaterThan(2)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("Title")
                .LessThan(256)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("Title");

            RuleFor(x => x.Publisher.Trim().Length)
                .LessThan(256)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("Publisher");

            RuleFor(x => x.Title.Trim())
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Language");

            RuleFor(x => x.Language.Trim().Length)
                .GreaterThan(1)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("Language")
                .LessThan(3)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("Language");

            RuleFor(x => x.Title.Trim())
               .NotEmpty()
               .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
               .OverridePropertyName("Isbn");

            RuleFor(x => x.Isbn.Trim().Length)
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .GreaterThan(9)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("Isbn")
                .LessThan(11)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("Isbn");

            RuleFor(x => x.Isbn)
                .Must(IsValidIsbn)
                .WithMessage(ErrorCode.InvalidIsbnCode.ToString())
                .WithName("Isbn");

            RuleFor(x => x.Description.Trim().Length)
                .LessThan(2000)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("Description");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .GreaterThanOrEqualTo(0)
                .WithMessage(ErrorCode.BelowMinimumValue.ToString())
                .WithName("Quantity")
                .LessThanOrEqualTo(1000000)
                .WithMessage(ErrorCode.AboveMaximumValue.ToString())
                .WithName("Quantity");

            RuleFor(x => x.Paperback)
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .GreaterThanOrEqualTo(0)
                .WithMessage(ErrorCode.BelowMinimumValue.ToString())
                .WithName("Paperback")
                .LessThanOrEqualTo(10000)
                .WithMessage(ErrorCode.AboveMaximumValue.ToString())
                .WithName("Paperback");

            RuleFor(x => x.AuthorKey)
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .WithName("AuthorKey")
                .Length(36, 36)
                .OverridePropertyName("AuthorKey")
                .WithMessage(ErrorCode.InvalidFormat.ToString());

            RuleFor(x => x.CategoryKey)
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .WithName("CategoryKey")
                .Length(36, 36)
                .OverridePropertyName("CategoryKey")
                .WithMessage(ErrorCode.InvalidFormat.ToString());

            RuleFor(x => x.PublishDate.Date)
                .LessThanOrEqualTo(new DateTime(800, 1, 1).Date)
                .WithMessage(ErrorCode.IncorrectDate.ToString())
                .WithName("PublishDate")
                .GreaterThanOrEqualTo(DateTime.Today.Date)
                .WithMessage(ErrorCode.IncorrectDate.ToString())
                .WithName("PublishDate");
        }

        /// <summary>
        /// Validates ISBN code
        /// </summary>
        /// <param name="isbn">Code to validate</param>
        /// <returns>True if valid</returns>
        private bool IsValidIsbn(string isbn)
        {
            if (!string.IsNullOrEmpty(isbn))
            {
                long j;
                if (isbn.Contains('-')) isbn = isbn.Replace("-", "");

                // Check if it contains any non numeric chars, if yes, return false
                if (!Int64.TryParse(isbn.Substring(0, isbn.Length - 1), out j))
                    return false;

                // Checking if the last char is not 'X' and 
                // and if it's a numeric value
                char lastChar = isbn[isbn.Length - 1];
                if (lastChar == 'X' && !Int64.TryParse(lastChar.ToString(), out j))
                    return false;

                int sum = 0;
                // Using the alternative way of calculation
                for (int i = 0; i < 9; i++)
                    sum += Int32.Parse(isbn[i].ToString()) * (i + 1);

                // Getting the remainder or the checkdigit
                int remainder = sum % 11;

                // Check if the checkdigit is same as the last number of ISBN 10 code
                return (remainder == int.Parse(isbn[9].ToString()));
            }
            return false;
        }
    }
}
