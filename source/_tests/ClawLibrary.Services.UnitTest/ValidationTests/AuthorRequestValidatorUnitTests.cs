using System.Linq;
using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Authors;
using ClawLibrary.Services.Validation.Authors;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ClawLibrary.Services.UnitTests.ValidationTests
{
    [TestFixture]
    public class AuthorRequestValidatorUnitTests
    {
        private AuthorRequestValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new AuthorRequestValidator();
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.TooShort)]
        [TestCase("LoremIpsumissimplydummytextoftheprintingandssasddd", ErrorCode.TooLong)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.TooShort)]
        [TestCase("Lorem Ipsumissi mplydummytex toftheprintingands sasddd", ErrorCode.TooLong)]
        public void Should_Have_Error_When_First_Name_Is_Wrong(string firstName, ErrorCode errorCode)
        {
            // arrange
            var request = new AuthorRequest { FirstName = firstName, LastName = "Test", Description = "ssimplydummytextoftheprintinga" };
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
            var request = new AuthorRequest { FirstName = "Test", LastName = lastName, Description = "ssimplydummytextoftheprintinga" };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.LastName, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "LastName" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase("Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32. Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32. Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (TheaExtremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", ErrorCode.TooLong)]
        [TestCase("Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheExtremesofGoodandEvil)byCicero,writtenin45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheExtremesofGoodandEvil)byCicero,writtenin45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheaExtremesofGoodandEvil)byCicero,writtenin45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.ThestandardchunkofLoremIpsumusedsincethe1500sisreproducedbelowforthoseinterested.Sections1.10.32and1.10.33from\"deFinibusBonorumetMalorum\"byCiceroarealsoreproducedintheirexactoriginalform,accompaniedbyEnglishversionsfromthe1914translationbyH.Rackham.", ErrorCode.TooLong)]
        [TestCase("Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheExtremesofGoodandEvil)byCicero,writtenins45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinlaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabiteraLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabturefrom45BC,makingitover2000yearsoaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabld.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheExtremesofGoodandEvil)byCicero,writtenin45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtab", ErrorCode.TooLong)]
        public void Should_Have_Error_When_Description_Is_Wrong(string description, ErrorCode errorCode)
        {
            // arrange
            var request = new AuthorRequest { FirstName = "Test", LastName = "Test", Description = description };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Description, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Description" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase("")]
        [TestCase("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.")]
        [TestCase("Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheExtremesofGoodandEvil)byCicero,writtenin45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinlaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabiteraLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabturefrom45BC,makingitover2000yearsoaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabld.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheExtremesofGoodandEvil)byCicero,writtenin45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtab")]
        public void Should_Not_Have_Error_When_Description_Is_Correct(string description)
        {
            // arrange
            var request = new AuthorRequest { FirstName = "Test", LastName = "Test", Description = description };
            // act  
            var result = _validator.Validate(request);
            // assert
            Assert.AreEqual(0, result.Errors.Count());
        }

        [TestCase("Test")]
        [TestCase("Contrarytopopularbeliefcontrarytopopularbeliefdac")]
        [TestCase("Schwimmer")]
        public void Should_Not_Have_Error_When_Last_Name_Is_Correct(string lastName)
        {
            // arrange
            var request = new AuthorRequest { FirstName = "Test", LastName = lastName, Description = "Test" };
            // act  
            var result = _validator.Validate(request);
            // assert
            Assert.AreEqual(0, result.Errors.Count());
        }

        [TestCase("Test")]
        [TestCase("Contrarytopopularbeliefcontrarytopopularbeliefdac")]
        [TestCase("David")]
        public void Should_Not_Have_Error_When_First_Name_Is_Correct(string firstName)
        {
            // arrange
            var request = new AuthorRequest { FirstName = firstName, LastName = "Test", Description = "Test" };
            // act  
            var result = _validator.Validate(request);
            // assert
            Assert.AreEqual(0, result.Errors.Count());
        }
    }
}
