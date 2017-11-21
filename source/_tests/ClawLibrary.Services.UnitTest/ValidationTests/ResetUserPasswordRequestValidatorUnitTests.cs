using System.Linq;
using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Users;
using ClawLibrary.Services.Validation.Users;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ClawLibrary.Services.UnitTests.ValidationTests
{
    [TestFixture]
    public class ResetUserPasswordRequestValidatorUnitTests
    {
        private ResetUserPasswordRequestValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ResetUserPasswordRequestValidator();
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.ValidationEmailFormat)]
        [TestCase("LoremIpsumissimplydummytextoftheprint", ErrorCode.ValidationEmailFormat)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.ValidationEmailFormat)]
        [TestCase("as@test", ErrorCode.ValidationEmailFormat)]
        [TestCase("@test.pl", ErrorCode.ValidationEmailFormat)]
        [TestCase("atest.pl", ErrorCode.ValidationEmailFormat)]
        public void Should_Have_Error_When_Email_Is_Wrong(string email, ErrorCode errorCode)
        {
            // arrange
            var request = new ResetUserPasswordRequest()
            {
                Email = email
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Email, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Email" && o.ErrorMessage == errorCode.ToString()));
        }

    }
}
