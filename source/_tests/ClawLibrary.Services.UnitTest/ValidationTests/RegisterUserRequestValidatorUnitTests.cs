using System.Linq;
using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Users;
using ClawLibrary.Services.Validation.Users;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ClawLibrary.Services.UnitTests.ValidationTests
{
    [TestFixture]
    public class RegisterUserRequestValidatorUnitTests
    {
        private RegisterUserRequestValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new RegisterUserRequestValidator();
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
            var request = new RegisterUserRequest()
            {
                Email = email,
                Password = "test1234test",
                ConfirmPassword = "test1234test",
                PhoneNumber = "789456123",
                FirstName = "David",
                LastName = "Schwimmer",
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
            var request = new RegisterUserRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = password,
                ConfirmPassword = password,
                PhoneNumber = "789456123",
                FirstName = "David",
                LastName = "Schwimmer",
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Password, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Password" && o.ErrorMessage == errorCode.ToString()));
        }

        [Test]
        public void Should_Have_Error_When_Confirm_Password_Is_Different_Than_Password()
        {
            // arrange
            var request = new RegisterUserRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = "test1234test",
                ConfirmPassword = "test234test",
                FirstName = "David",
                LastName = "Schwimmer",
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.ConfirmPassword, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "ConfirmPassword" && o.ErrorMessage == ErrorCode.ValidationPasswordDoesNotMatch.ToString()));
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.TooShort)]
        [TestCase("LoremIpsumissimplydummytextoftheprintingandssasdddssssssssssmissimplydummytextoftheprintsssssss", ErrorCode.TooLong)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.TooShort)]
        [TestCase("Lorem Ipsumissi mplydummytex toftheprintingands sasddd missimplydummytextoftheprint missimplydummytextoftheprint", ErrorCode.TooLong)]
        public void Should_Have_Error_When_First_Name_Is_Wrong(string firstName, ErrorCode errorCode)
        {
            // arrange
            var request = new RegisterUserRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = "test1234test",
                ConfirmPassword = "test1234test",
                FirstName = firstName,
                LastName = "Schwimmer",
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.FirstName, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "FirstName" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.TooShort)]
        [TestCase("LoremIpsumissimplydummytextoftheprintingandssasddd", ErrorCode.TooLong)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.TooShort)]
        [TestCase("asd", ErrorCode.TooShort)]
        [TestCase("Lorem Ipsumissi mplydummytex toftheprintingands sasddd", ErrorCode.TooLong)]
        public void Should_Have_Error_When_Last_Name_Is_Wrong(string lastName, ErrorCode errorCode)
        {
            // arrange
            var request = new RegisterUserRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = "test1234test",
                ConfirmPassword = "test1234test",
                FirstName = "David",
                LastName = lastName,
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.LastName, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "LastName" && o.ErrorMessage == errorCode.ToString()));
        }
    }
}
