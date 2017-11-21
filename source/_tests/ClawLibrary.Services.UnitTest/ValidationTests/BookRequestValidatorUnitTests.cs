using System;
using System.Linq;
using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Books;
using ClawLibrary.Services.Validation.Books;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace ClawLibrary.Services.UnitTests.ValidationTests
{
    [TestFixture]
    public class BookRequestValidatorUnitTests
    {
        private BookRequestValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new BookRequestValidator();
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.TooShort)]
        [TestCase("LoremIpsumissimplydumLoremIpsddumissimplydummytextoftheprintingandssasdddmytextoftheprintingsandssasdddLoremIpsumissimplydummytextoftheprintingandsLoremIpsumissimplLoremIpsumissimplydummytextoftheprintingandssasdddyduammytextoftheprinsftingandssasdddsasddd", ErrorCode.TooLong)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.TooShort)]
        [TestCase("LoremIpsumis simplydumLoremIpsddumiss implydummytext oftheprintingandssasdddmyt extoftheprinting sandssasdddLoremIpsumissimplydummytextoftheprintingan dsLoremIpsumissimplLore mIpsumissimplydummyte xtoftheprintingandssasdddyduammytextoftheprinsftingandssasdddsasddd", ErrorCode.TooLong)]
        public void Should_Have_Error_When_Title_Is_Wrong(string title, ErrorCode errorCode)
        {
            // arrange
            var request = new BookRequest()
            {
                Title = title,
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "9780747560722",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                AuthorKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                CategoryKey = "0E63BE9D-37C9-4C38-86FC-68A85593CD93"
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Title, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Title" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.TooShort)]
        [TestCase("LoremIpsumissimplydumLoremIpsddumissimplydummytextoftheprintingandssasdddmytextoftheprintingsandssasdddLoremIpsumissimplydummytextoftheprintingandsLoremIpsumissimplLoremIpsumissimplydummytextoftheprintingandssasdddyduammytextoftheprinsftingandssasdddsasddd", ErrorCode.TooLong)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.TooShort)]
        [TestCase("LoremIpsumis simplydumLoremIpsddumiss implydummytext oftheprintingandssasdddmyt extoftheprinting sandssasdddLoremIpsumissimplydummytextoftheprintingan dsLoremIpsumissimplLore mIpsumissimplydummyte xtoftheprintingandssasdddyduammytextoftheprinsftingandssasdddsasddd", ErrorCode.TooLong)]
        public void Should_Have_Error_When_Publisher_Is_Wrong(string publisher, ErrorCode errorCode)
        {
            // arrange
            var request = new BookRequest()
            {
                Title = "Ward and Sons",
                Publisher = publisher,
                Language = "DE",
                Isbn = "9780747560722",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                AuthorKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                CategoryKey = "0E63BE9D-37C9-4C38-86FC-68A85593CD93"
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Publisher, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Publisher" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.TooShort)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("ads", ErrorCode.TooLong)]
        public void Should_Have_Error_When_Language_Is_Wrong(string language, ErrorCode errorCode)
        {
            // arrange
            var request = new BookRequest()
            {
                Title = "Ward and Sons",
                Publisher = "Ward and Sons",
                Language = language,
                Isbn = "9780747560722",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                AuthorKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                CategoryKey = "0E63BE9D-37C9-4C38-86FC-68A85593CD93"
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Language, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Language" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.TooShort)]
        [TestCase("adsadsads", ErrorCode.TooShort)]
        [TestCase("716-85-851", ErrorCode.TooShort)]
        [TestCase("adsadsadsadssd", ErrorCode.TooLong)]
        [TestCase("716-85-851-985-965", ErrorCode.TooLong)]
        [TestCase("9280747560722", ErrorCode.InvalidIsbnCode)]
        public void Should_Have_Error_When_Isbn_Is_Wrong(string isbn, ErrorCode errorCode)
        {
            // arrange
            var request = new BookRequest()
            {
                Title = "Deleniti",
                Publisher = "Ward and Sons",
                Language = "PL",
                Isbn = isbn,
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                AuthorKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                CategoryKey = "0E63BE9D-37C9-4C38-86FC-68A85593CD93"
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Isbn, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Isbn" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase("Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32. Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32. Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (TheaExtremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet..\", comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from \"de Finibus Bonorum et Malorum\" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.", ErrorCode.TooLong)]
        [TestCase("Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheExtremesofGoodandEvil)byCicero,writtenin45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheExtremesofGoodandEvil)byCicero,writtenin45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheaExtremesofGoodandEvil)byCicero,writtenin45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.ThestandardchunkofLoremIpsumusedsincethe1500sisreproducedbelowforthoseinterested.Sections1.10.32and1.10.33from\"deFinibusBonorumetMalorum\"byCiceroarealsoreproducedintheirexactoriginalform,accompaniedbyEnglishversionsfromthe1914translationbyH.Rackham.", ErrorCode.TooLong)]
        [TestCase("Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheExtremesofGoodandEvil)byCicero,writtenins45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinlaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabiteraLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabturefrom45BC,makingitover2000yearsoaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabld.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtablesource.LoremIpsumcomesfromsections1.10.32and1.10.33of\"deFinibusBonorumetMalorum\"(TheExtremesofGoodandEvil)byCicero,writtenin45BC.Thisbookisatreatiseonthetheoryofethics,verypopularduringtheRenaissance.ThefirstlineofLoremIpsum,\"Loremipsumdolorsitamet..\",comesfromalineinsection1.10.32.Contrarytopopularbelief,LoremIpsumisnotsimplyrandomtext.IthasrootsinapieceofclassicalLatinliteraturefrom45BC,makingitover2000yearsold.RichardMcClintock,aLatinprofessoratHampden-SydneyCollegeinVirginia,lookeduponeofthemoreobscureLatinwords,consectetur,fromaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtabaLoremIpsumpassage,andgoingthroughthecitesofthewordinclassicalliterature,discoveredtheundoubtab", ErrorCode.TooLong)]
        public void Should_Have_Error_When_Description_Is_Wrong(string description, ErrorCode errorCode)
        {
            // arrange
            var request = new BookRequest()
            {
                Title = "Deleniti",
                Publisher = "Ward and Sons",
                Language = "PL",
                Isbn = "9780747560722",
                Description = description,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                AuthorKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                CategoryKey = "0E63BE9D-37C9-4C38-86FC-68A85593CD93"
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Description, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Description" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase(-1, ErrorCode.BelowMinimumValue)]
        [TestCase(-11000, ErrorCode.BelowMinimumValue)]
        [TestCase(10001, ErrorCode.AboveMaximumValue)]
        [TestCase(long.MinValue, ErrorCode.BelowMinimumValue)]
        [TestCase(long.MaxValue, ErrorCode.AboveMaximumValue)]
        public void Should_Have_Error_When_Paperback_Is_Wrong(long paperback, ErrorCode errorCode)
        {
            // arrange
            var request = new BookRequest()
            {
                Title = "Deleniti",
                Publisher = "Ward and Sons",
                Language = "PL",
                Isbn = "9780747560722",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = paperback,
                PublishDate = new DateTime(1959, 2, 17),
                AuthorKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                CategoryKey = "0E63BE9D-37C9-4C38-86FC-68A85593CD93"
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.Paperback, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "Paperback" && o.ErrorMessage == errorCode.ToString()));
        }

        [Test]
        public void Should_Have_Error_When_PublishDate_Is_Too_Old()
        {
            // arrange
            var request = new BookRequest()
            {
                Title = "Deleniti",
                Publisher = "Ward and Sons",
                Language = "PL",
                Isbn = "9780747560722",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 256,
                PublishDate = new DateTime(799, 12, 31),
                AuthorKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                CategoryKey = "0E63BE9D-37C9-4C38-86FC-68A85593CD93"
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.PublishDate, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "PublishDate" && o.ErrorMessage == ErrorCode.IncorrectDate.ToString()));
        }

        [Test]
        public void Should_Have_Error_When_PublishDate_Is_In_Future()
        {
            // arrange
            var request = new BookRequest()
            {
                Title = "Deleniti",
                Publisher = "Ward and Sons",
                Language = "PL",
                Isbn = "9780747560722",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 256,
                PublishDate = new DateTime(2799, 12, 31),
                AuthorKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                CategoryKey = "0E63BE9D-37C9-4C38-86FC-68A85593CD93"
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.PublishDate, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "PublishDate" && o.ErrorMessage == ErrorCode.IncorrectDate.ToString()));
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.InvalidFormat)]
        [TestCase("2E218BD7-D56D-428E-8765-64C3825D3EE", ErrorCode.InvalidFormat)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.InvalidFormat)]
        [TestCase("2E218BD7-D56D-428E-8765-64C3825D3EEFD", ErrorCode.InvalidFormat)]
        [TestCase("2E218BD7D56D428E876564C3825D3EEF", ErrorCode.InvalidFormat)]
        public void Should_Have_Error_When_Author_Key_Is_Wrong(string authorKey, ErrorCode errorCode)
        {
            // arrange
            var request = new BookRequest()
            {
                Title = "Test",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "9780747560722",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                AuthorKey = authorKey,
                CategoryKey = "0E63BE9D-37C9-4C38-86FC-68A85593CD93"
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.AuthorKey, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "AuthorKey" && o.ErrorMessage == errorCode.ToString()));
        }

        [TestCase("", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("a", ErrorCode.InvalidFormat)]
        [TestCase("2E218BD7-D56D-428E-8765-64C3825D3EE", ErrorCode.InvalidFormat)]
        [TestCase("     ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase(" ", ErrorCode.CannotBeNullOrEmpty)]
        [TestCase("as", ErrorCode.InvalidFormat)]
        [TestCase("2E218BD7-D56D-428E-8765-64C3825D3EEFDD", ErrorCode.InvalidFormat)]
        [TestCase("2E218BD7D56D428E876564C3825D3EEF", ErrorCode.InvalidFormat)]
        public void Should_Have_Error_When_Category_Key_Is_Wrong(string categoryKey, ErrorCode errorCode)
        {
            // arrange
            var request = new BookRequest()
            {
                Title = "Test",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "9780747560722",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                AuthorKey = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                CategoryKey = categoryKey
            };
            // act  
            var result = _validator.ShouldHaveValidationErrorFor(x => x.CategoryKey, request);
            // assert
            Assert.That(result.Any(o => o.PropertyName == "CategoryKey" && o.ErrorMessage == errorCode.ToString()));
        }
    }
}
