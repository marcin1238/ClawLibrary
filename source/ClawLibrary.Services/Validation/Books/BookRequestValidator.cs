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
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Title")
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

            RuleFor(x => x.Publisher.Trim())
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Publisher")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Publisher");

            RuleFor(x => x.Publisher.Trim().Length)
                .GreaterThan(2)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("Publisher")
                .LessThan(256)
                .WithMessage(ErrorCode.TooLong.ToString())
                .OverridePropertyName("Publisher");

            RuleFor(x => x.Language.Trim())
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Language")
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

            RuleFor(x => x.Isbn.Trim())
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Isbn")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Isbn");

            RuleFor(x => x.Isbn.Trim().Replace("-", "").Length)
                .GreaterThan(9)
                .WithMessage(ErrorCode.TooShort.ToString())
                .OverridePropertyName("Isbn")
                .LessThan(14)
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

            RuleFor(x => x.Paperback)
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Paperback")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("Paperback")
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .GreaterThanOrEqualTo(0)
                .WithMessage(ErrorCode.BelowMinimumValue.ToString())
                .WithName("Paperback")
                .LessThanOrEqualTo(10000)
                .WithMessage(ErrorCode.AboveMaximumValue.ToString())
                .WithName("Paperback");

            RuleFor(x => x.AuthorKey)
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("AuthorKey")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("AuthorKey")
                .WithName("AuthorKey")
                .Length(36, 36)
                .OverridePropertyName("AuthorKey")
                .WithMessage(ErrorCode.InvalidFormat.ToString());

            RuleFor(x => x.CategoryKey)
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("CategoryKey")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("CategoryKey")
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .WithName("CategoryKey")
                .Length(36, 36)
                .OverridePropertyName("CategoryKey")
                .WithMessage(ErrorCode.InvalidFormat.ToString());

            RuleFor(x => x.PublishDate)
                .NotNull()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("PublishDate")
                .NotEmpty()
                .WithMessage(ErrorCode.CannotBeNullOrEmpty.ToString())
                .OverridePropertyName("PublishDate")
                .Must(BeAValidPublishDate)
                .WithMessage(ErrorCode.IncorrectDate.ToString())
                .WithName("PublishDate");
        }

        /// <summary>
        /// Validates publish date
        /// </summary>
        /// <param name="date">Date to validate</param>
        /// <returns>True if valid</returns>
        private bool BeAValidPublishDate(DateTime date)
        {
            return date >= new DateTime(800, 1, 1) && date <= DateTime.Today;
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
                if (isbn.Trim().Replace("-", "").Length == 10)
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
                else if (isbn.Trim().Replace("-", "").Length == 13)
                {
                    long j;
                    if (isbn.Contains('-')) isbn = isbn.Replace("-", "");

                    // Check if it contains any non numeric chars, if yes, return false
                    if (!Int64.TryParse(isbn, out j))
                        return false;

                    int sum = 0;
                    // Comment Source: Wikipedia
                    // The calculation of an ISBN-13 check digit begins with the first
                    // 12 digits of the thirteen-digit ISBN (thus excluding the check digit itself).
                    // Each digit, from left to right, is alternately multiplied by 1 or 3,
                    // then those products are summed modulo 10 to give a value ranging from 0 to 9.
                    // Subtracted from 10, that leaves a result from 1 to 10. A zero (0) replaces a
                    // ten (10), so, in all cases, a single check digit results.
                    for (int i = 0; i < 12; i++)
                    {
                        sum += Int32.Parse(isbn[i].ToString()) * (i % 2 == 1 ? 3 : 1);
                    }

                    int remainder = sum % 10;
                    int checkDigit = 10 - remainder;
                    if (checkDigit == 10) checkDigit = 0;

                    return (checkDigit == int.Parse(isbn[12].ToString()));
                }
            }
            return false;
        }
    }
}
