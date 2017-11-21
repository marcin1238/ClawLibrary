using System.Linq;
using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Categories;
using ClawLibrary.Services.Validation.Categories;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ClawLibrary.Services.UnitTests.ValidationTests
{
    [TestFixture]
    public class CategoryRequestValidatorUnitTests
    {
        private CategoryRequestValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CategoryRequestValidator();
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.TooShort)]
        [TestCase("LoremIpsumissimplydummytextoftheprintingandssasdddLoremIpsumissimplydummytextoftheprintingandssasdddLoremIpsumissimplydummytextoftheprintingandssasdddLoremIpsumissimplydummytextoftheprintingandssasdddLoremIpsumissimplydummytextoftheprintingandssasdddLoremIpsumissimplydummytextoftheprintingandssasddd", ErrorCode.TooLong)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.TooShort)]
        [TestCase("Lorem Ipsumissi mplydummytex toftheprintingands sasddd Lorem Ipsumissi mplydummytex toftheprintingands sasddd Lorem Ipsumissi mplydummytex toftheprintingands sasddd Lorem Ipsumissi mplydummytex toftheprintingands sasddd Lorem Ipsumissi mplydummytex toftheprintingands sasddd Lorem Ipsumissi mplydummytex toftheprintingands sasddd", ErrorCode.TooLong)]
        public void Should_Have_Error_When_First_Name_Is_Wrong(string name, ErrorCode errorCode)
        {
            // arrange
            var request = new CategoryRequest() { Name = name };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Name, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Name" && o.ErrorMessage == errorCode.ToString()));
        }


    }
}
