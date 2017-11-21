using System.Linq;
using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Users;
using ClawLibrary.Services.Validation.Users;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ClawLibrary.Services.UnitTests.ValidationTests
{
    [TestFixture]
    public class AuthorizeRequestValidatorUnitTests
    {
        private AuthorizeRequestValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new AuthorizeRequestValidator();
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
            var request = new AuthorizeRequest()
            {
                Email = email,
                Password = "test1234test",
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Email, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Email" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.ValidationPasswordLength)]
        [TestCase("LoremIpsumissimplydummytextoftheprintLoremIpsumissimplydummytextoftheprintLoremIpsumissimplydummytextoftheprintLoremIpsumissimplydummytextoftheprintLoremIpsumissimplydummytextoftheprint", ErrorCode.ValidationPasswordLength)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.ValidationPasswordLength)]
        [TestCase("ast", ErrorCode.ValidationPasswordLength)]
        [TestCase("testpl", ErrorCode.ValidationPasswordLength)]
        [TestCase("atestpl", ErrorCode.ValidationPasswordLength)]
        public void Should_Have_Error_When_Password_Is_Wrong(string password, ErrorCode errorCode)
        {
            // arrange
            var request = new AuthorizeRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = password
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Password, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Password" && o.ErrorMessage == errorCode.ToString()));
        }
    }
}
