using System.Linq;
using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Users;
using ClawLibrary.Services.Validation.Users;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ClawLibrary.Services.UnitTests.ValidationTests
{
    [TestFixture]
    public class UserVerificationRequestValidatorUnitTests
    {
        private UserVerificationRequestValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UserVerificationRequestValidator();
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
            var request = new UserVerificationRequest()
            {
                Email = email,
                Password = "test1234test",
                VerificationCode = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"

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
            var request = new UserVerificationRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = password,
                VerificationCode = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"

            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Password, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Password" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.InvalidFormat)]
        [TestCase("2E218BD7-D56D-428E-8765-64C3825D3EE", ErrorCode.InvalidFormat)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.InvalidFormat)]
        [TestCase("2E218BD7-D56D-428E-8765-64C3825D3EEFD", ErrorCode.InvalidFormat)]
        [TestCase("2E218BD7D56D428E876564C3825D3EEF", ErrorCode.InvalidFormat)]
        public void Should_Have_Error_When_Author_Key_Is_Wrong(string verificationCode, ErrorCode errorCode)
        {
            // arrange
            var request = new UserVerificationRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = "test1234test",
                VerificationCode = verificationCode

            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.VerificationCode, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "VerificationCode" && o.ErrorMessage == errorCode.ToString()));
        }
    }
}
