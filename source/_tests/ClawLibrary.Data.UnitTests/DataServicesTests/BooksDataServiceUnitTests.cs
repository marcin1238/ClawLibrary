using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Data.DataServices;
using ClawLibrary.Data.Mapping;
using ClawLibrary.Data.Models;
using ClawLibrary.Data.UnitTests.Helpers;
using Moq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Book = ClawLibrary.Data.Models.Book;

namespace ClawLibrary.Data.UnitTests.DataServicesTests
{
    [TestFixture]
    public class BooksDataServiceUnitTests
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        private List<Book> _data;

        public BooksDataServiceUnitTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DataMappingProfile());
            }));
        }

        [SetUp]
        protected void SetUp()
        {
            _data = new List<Book>()
            {
                new Book() { Id  = 1, Key  = new Guid("6A0734F0-B3DE-44D6-93E3-F9E504A35A6E"), Title  = "Licensed Tasty Wooden Bike 78", Publisher  = "Fay Group", Language  = "PL", Isbn  = "40446591",Description  = "Quam voluptates modi labore atem voluptas quis iste quibusdam maiores laudantium dolorem provident amet deleniti quisquam distinctio omnis.", Quantity  = 97, Paperback  = 409, PublishDate  = new DateTime(1929,12,22), AuthorId  = 2,ImageFileId  = null, CategoryId  = 23, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = new DateTimeOffset(new DateTime(2017,9,12)), ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 2, Key = new Guid("66C50043-BD59-4B74-B8C3-00EF22712324"), FirstName = "Floy", LastName = "Stroman",Description = "Sunt optioatione accusantium consequaturui atque adipisci est ipsum est vitae reiciendis consectetur dolores deleniti sapiente.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 23, Key = new Guid("4D8C4A98-8B78-417B-BDCE-84799C2D875B"), Name = "Cookbooks", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 2, Key  = new Guid("7FB56B07-18E8-4F01-9FB8-5A474A59ADCC"), Title  = "Generic Incredible Frozen Table 136", Publisher  = "Boyle Inc", Language  = "PL", Isbn  = "13486737",Description  = "Rem sed inventore aut v nemo nihil similique et autem tenetur illum fugiat et iste ipsum provident sint sed quis voluptas labore et consequuntur facere eum eveniet nam beatae qui.", Quantity  = 96, Paperback  = 557, PublishDate  = new DateTime(1993,11,1), AuthorId  = 2,ImageFileId  = null, CategoryId  = 2, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,10)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 2, Key = new Guid("66C50043-BD59-4B74-B8C3-00EF22712324"), FirstName = "Floy", LastName = "Stroman",Description = "Sunt lis ratione accusantium conseqlat qui atque adipisci est ipsum est vitae reiciendis consectetur dolores deleniti sapiente.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 2, Key = new Guid("4D4393B4-A6F6-40AB-A18D-96703462496E"), Name = "Satire", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 3, Key  = new Guid("8BEB61E6-50CA-417E-9784-690E32B905F6"), Title  = "Small Practical Soft Bacon 479", Publisher  = "Schmidt, Hills and Brown", Language  = "DE", Isbn  = "08178053",Description  = "Numquam molliiat error pariatur molestiae neque omnis officia qui iste incidunt non.", Quantity  = 82, Paperback  = 857, PublishDate  = new DateTime(1938,11,25), AuthorId  = 2,ImageFileId  = null, CategoryId  = 28, CreatedDate  = new DateTimeOffset(new DateTime(2017,10,12)), CreatedBy  = "System",ModifiedDate  = new DateTimeOffset(new DateTime(2017,10,12)), ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 2, Key = new Guid("66C50043-BD59-4B74-B8C3-00EF22712324"), FirstName = "Floy", LastName = "Stroman",Description = "Sunt optio unde expedita omnis qui et sunt modiumenda saepe deleniti accusamu reiciendis consectetur dolores deleniti sapiente.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 28, Key = new Guid("4EA06D28-07FE-4E72-BFF4-BA2FBFB390C7"), Name = "Trilogy", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 4, Key  = new Guid("433470CD-6E7D-4D2D-9FE7-C0FA483E93FF"), Title  = "Rustic Incredible Soft Bike 949", Publisher  = "Quigley - Bartell", Language  = "PL", Isbn  = "14533162",Description  = "Ut consequatur quaesdam.", Quantity  = 30, Paperback  = 315, PublishDate  = new DateTime(1927,11,26), AuthorId  = 3,ImageFileId  = null, CategoryId  = 9, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 3, Key = new Guid("95977A1F-DDA4-4D2A-BE82-188626D48055"), FirstName = "Jerrell", LastName = "Farrell",Description = "Voluptates repellendus sapiente molestiae aut sunt aut amet vitae neque sint ipsa eum vero debitis doloremque eligendi dolores et quibusdam quam in accueum blanditiis earum architectt itaque.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 9, Key = new Guid("45C914D1-4FAB-4650-B5C3-30CAC39BA7C9"), Name = "Health", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 5, Key  = new Guid("6968DB0F-6059-4B23-865A-317D70F46268"), Title  = "Intelligent Rustic Cotton Pizza 534", Publisher  = "Bailey Inc", Language  = "EN", Isbn  = "36237977",Description  = "Modi optio eaque sapieo voluptatem hic non expedita necessitatibus tenetur aut.", Quantity  = 5, Paperback  = 544, PublishDate  = new DateTime(1985,3,22), AuthorId  = 3,ImageFileId  = null, CategoryId  = 29, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 3, Key = new Guid("95977A1F-DDA4-4D2A-BE82-188626D48055"), FirstName = "Jerrell", LastName = "Farrell",Description = "Voluptates repellendus sapiente molestiae aut sunt aut amet vitae neque sint ipsa eum vero debitis dollat labore mollitia officia dcupiditate repellat enim et adipisci itaque deserunt itaque.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 29, Key = new Guid("51E555F3-7033-407C-9DC1-75EA8279F1D6"), Name = "Biographies", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 6, Key  = new Guid("A63DAEB5-C6CB-4F63-9F36-46E1617CF6EA"), Title  = "Incredible Small Granite Bike 465", Publisher  = "Koelpin, Tremblay and Barton", Language  = "PL", Isbn  = "57673807",Description  = "Dolorucabo assumenda dignissimos nulla necessitatibus ipsam voluptatem ex molestiae distinctio ex culpa non.", Quantity  = 41, Paperback  = 352, PublishDate  = new DateTime(1942,4,13), AuthorId  = 3,ImageFileId  = null, CategoryId  = 11, CreatedDate  = new DateTimeOffset(new DateTime(2017,10,11)), CreatedBy  = "System",ModifiedDate  = new DateTimeOffset(new DateTime(2017,10,11)), ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 3, Key = new Guid("95977A1F-DDA4-4D2A-BE82-188626D48055"), FirstName = "Jerrell", LastName = "Farrell",Description = "Voluptates repsciunt iusto aut porro quis neia officia dolores quae ut dolor eum blanditiis earum architecto consequatur est nobis cupiditate repellat enim et adipisci itaque deserunt itaque.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 11, Key = new Guid("A65E7C3B-CCDF-40D9-92C8-5ACCB9580BF0"), Name = "Travel", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 7, Key  = new Guid("C0A39D4E-CBAC-4768-A42F-D93D258934D3"), Title  = "Unbranded Handcrafted Plastic Towels 622", Publisher  = "Davis - Zulauf", Language  = "PL", Isbn  = "12619780",Description  = "Porro nulla mnda sapiente optio fuga et consequatur aut eos amet.", Quantity  = 25, Paperback  = 655, PublishDate  = new DateTime(1952,1,19), AuthorId  = 3,ImageFileId  = null, CategoryId  = 13, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 3, Key = new Guid("95977A1F-DDA4-4D2A-BE82-188626D48055"), FirstName = "Jerrell", LastName = "Farrell",Description = "Voluptates repellendus sapiente molestiae aut sunt aut amet vitae neque sint ipsa eum vero debitis dolore labore mollitia officia dolorditate repellat enim et adipisci itaque deserunt itaque.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 13, Key = new Guid("6307D58F-E2BE-466B-89FC-53147B64283D"), Name = "Religion, Spirituality & New Age", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 8, Key  = new Guid("E11EBF20-3D98-43A9-B711-63F85C05A36B"), Title  = "Licensed Intelligent Plastic Chicken 131", Publisher  = "OConnell - Pfeffer", Language  = "DE", Isbn  = "64012402",Description  = "Ut perfer sed quaerat aut consequuntur velit nisi molestiae.", Quantity  = 79, Paperback  = 856, PublishDate  = new DateTime(1984,12,16), AuthorId  = 4,ImageFileId  = null, CategoryId  = 2, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 4, Key = new Guid("2E218BD7-D56D-428E-8765-64C3825D3EEF"), FirstName = "Princess", LastName = "Hessel", Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 2, Key = new Guid("4D4393B4-A6F6-40AB-A18D-96703462496E"), Name = "Satire", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 9, Key  = new Guid("CDD053B4-6E2C-4CA1-8B17-BD1A3B919A51"), Title  = "Awesome Awesome Rubber Hat 62", Publisher  = "Kuhn - Ward", Language  = "DE", Isbn  = "88647140",Description  = "Ex dolores corrupti ducimuss est.", Quantity  = 41, Paperback  = 350, PublishDate  = new DateTime(1947,10,21), AuthorId  = 4,ImageFileId  = null, CategoryId  = 10, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 4, Key = new Guid("2E218BD7-D56D-428E-8765-64C3825D3EEF"), FirstName = "Princess", LastName = "Hessel", Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 10, Key = new Guid("E7F716C2-FA5E-433C-A3C3-478F1B94E44B"), Name = "Guide", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 10, Key  = new Guid("8EFA1076-7905-45DB-A8CB-CDEB792C1442"), Title  = "Ergonomic Small Metal Sausages 951", Publisher  = "Wiza - Schuppe", Language  = "PL", Isbn  = "10573912",Description  = "Molestiae et iste  autem atque dolor quia culpa aliquam enim qui tempora quia maxime voluptas.", Quantity  = 53, Paperback  = 600, PublishDate  = new DateTime(1920,10,13), AuthorId  = 4,ImageFileId  = null, CategoryId  = 15, CreatedDate  = new DateTimeOffset(new DateTime(2017,10,9)), CreatedBy  = "System",ModifiedDate  = new DateTimeOffset(new DateTime(2017,10,9)), ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 4, Key = new Guid("2E218BD7-D56D-428E-8765-64C3825D3EEF"), FirstName = "Princess", LastName = "Hessel",Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 15, Key = new Guid("0E63BE9D-37C9-4C38-86FC-68A85593CD93"), Name = "History", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 11, Key  = new Guid("D1BBA9A4-5613-480C-9424-FDB4933D2E82"), Title  = "Practical Sleek Rubber Sausages 181", Publisher  = "Wunsch, Kris and Koepp", Language  = "EN", Isbn  = "12773876",Description  = "Velit platas et odit neque id ut aut rerum.", Quantity  = 82, Paperback  = 512, PublishDate  = new DateTime(1922,8,20), AuthorId  = 5,ImageFileId  = 1, CategoryId  = 27, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Active", Order  = new List<Order>(),Author  = new Author() { Id = 5, Key = new Guid("D2740B82-FD37-4A42-8ACA-0A6A61615645"), FirstName = "Alexandria", LastName = "Hartmann",Description = "Dolor sunt sint id repellendus ut perspiciatis sequi ad natus vel iste placeat adipisci autem nihil voluptatibus ex quia bmque rerum.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 27, Key = new Guid("94A01CBA-0011-4BD3-BBAC-EE83B17068E2"), Name = "Series", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = new File(){ FileName = "testFileName", Key = Guid.NewGuid(), Id = 1, CreatedBy = "System",CreatedDate = DateTimeOffset.Now,Status = Status.Active.ToString()}},
                new Book() { Id  = 29, Key  = new Guid("D68D5372-2E78-4C92-BE84-D34F240BF75A"), Title  = "Rustic Unbranded Fresh Fish 880", Publisher  = "Kutch - Volkman", Language  = "PL", Isbn  = "10579841",Description  = "Fuga odit maiores qu sit ratione quas odit ipsum.", Quantity  = 32, Paperback  = 128, PublishDate  = new DateTime(1935,8,7), AuthorId  = 11,ImageFileId  = null, CategoryId  = 4, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Pending", Order  = new List<Order>(),Author  = new Author() { Id = 11, Key = new Guid("E9862AF8-0017-4C36-B424-68E2D39D10DF"), FirstName = "Arvel", LastName = "Greenfelder",Description = "Et qui dolorem ratione aut quisquam voluptatum quaerat est omnis non nostrum qui suscipit vero eius maxime a enim sint ablendus.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 4, Key = new Guid("F42E66C2-89CE-4D27-92CF-2348B8B21348"), Name = "Action and Adventure", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 30, Key  = new Guid("657E8BB0-3F33-4A92-B05E-43D977CBBC44"), Title  = "Sleek Ergonomic Granite Bacon 151", Publisher  = "Thiel Group", Language  = "DE", Isbn  = "22289594",Description  = "Alias odio impedit aspunt ut numquam dolorem dolore qui sed eveniet consequatur doloribus aut facere laborum officia.", Quantity  = 11, Paperback  = 368, PublishDate  = new DateTime(1993,6,30), AuthorId  = 11,ImageFileId  = null, CategoryId  = 4, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Inactive", Order  = new List<Order>(),Author  = new Author() { Id = 11, Key = new Guid("E9862AF8-0017-4C36-B424-68E2D39D10DF"), FirstName = "Arvel", LastName = "Greenfelder",Description = "Et qui dolorem ratione aut quisquam voluptatum quaerat porro saepe nobis autem voluptatibus recusandae aperiam iure repellendus.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 4, Key = new Guid("F42E66C2-89CE-4D27-92CF-2348B8B21348"), Name = "Action and Adventure", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
                new Book() { Id  = 31, Key  = new Guid("2E6C91BE-A96F-4F72-9644-90D0EEC7DEE3"), Title  = "Rustic Unbranded Wooden Hat 391", Publisher  = "Legros, Cormier and Nikolaus", Language  = "DE", Isbn  = "45611258",Description  = "Quibusdectus magnam nihil consequuntur veritatis voluptas amet esse voluptate praesentium ratione ullam quia.", Quantity  = 7, Paperback  = 223, PublishDate  = new DateTime(2016,4,9), AuthorId  = 12,ImageFileId  = null, CategoryId  = 7, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy  = "System",ModifiedDate  = null, ModifiedBy  = null, Status  = "Deleted", Order  = new List<Order>(),Author  = new Author() { Id = 12, Key = new Guid("D47AD167-7C15-4CAA-BB69-53C5EF0CA20D"), FirstName = "Dane", LastName = "Schumm",Description = "Harum ut accusamus laboriosam sint eveniet maxime expedatione quas nihil.", ImageFileId = null, CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)), CreatedBy = "System",ModifiedDate = null, ModifiedBy = null, Status = "Active"},Category = new Category{Id = 7, Key = new Guid("CEB7EF1E-AA0E-4D91-9374-A15F028F3BEA"), Name = "Horror", CreatedDate  = new DateTimeOffset(new DateTime(2017,9,11)),CreatedBy = "System", ModifiedDate = null, ModifiedBy = null, Status = "Active"}, ImageFile = null},
            };

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DatabaseContext(options);

            foreach (var book in _data)
            {
                context.Book.Add(book);
            }
            context.SaveChanges();
            _context = context;
        }

        [TestCase("D1BBA9A4-5613-480C-9424-FDB4933D2E82", 11)]
        [TestCase("E11EBF20-3D98-43A9-B711-63F85C05A36B", 8)]
        [TestCase("433470CD-6E7D-4D2D-9FE7-C0FA483E93FF", 4)]
        [TestCase("8BEB61E6-50CA-417E-9784-690E32B905F6", 3)]
        public async Task Should_Return_Book_With_Specified_Key(string bookKey, int expectedId)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);

            // act
            var book = await booksDataService.GetBookByKey(bookKey);

            // assert
            Assert.NotNull(book);
            Assert.AreEqual(expectedId, book.Id);
        }

        [TestCase("wrongBookKey")]
        [TestCase("    ")]
        [TestCase("123123213")]
        public async Task Should_Return_Null_When_Book_Key_Is_Wrong(string bookKey)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);

            // act
            var book = await booksDataService.GetBookByKey(bookKey);

            // assert
            Assert.Null(book);
        }

        [Test]
        public void Should_Throw_Exception_When_Book_Key_Is_Null()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.GetBookByKey(null));
        }

        [Test]
        public async Task Should_Return_Null_When_Book_Status_Is_Deleted()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);

            // act
            var book = await booksDataService.GetBookByKey("2E6C91BE-A96F-4F72-9644-90D0EEC7DEE3");

            // assert
            Assert.Null(book);
        }

        [Test]
        public void Should_Throw_Exception_When_Book_Key_Is_Empty()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.GetBookByKey(string.Empty));
        }

        [Test]
        public async Task Should_Create_New_Book()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Small Unbranded Cotton Tuna 258",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "96333500",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };

            // act
            var actualBook = await booksDataService.CreateBook(expectedBook);

            // assert
            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Quantity, actualBook.Quantity);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.AreEqual(Status.Active.ToString(), actualBook.Status);
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Author.Description, actualBook.Author.Description);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
         }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Title_Already_Exist()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Licensed Tasty Wooden Bike 78",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "63337014",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Isbn_Already_Exist()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Licensed Tasty Wooden Bike 78",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "40446591",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Title_Already_Exist_With_Status_Pending()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Rustic Unbranded Fresh Fish 880",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "63337014",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Isbn_Already_Exist_With_Status_Pending()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Rustic Unbranded Fresh Fish 880",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "10579841",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Title_Already_Exist_With_Status_Inactive()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Sleek Ergonomic Granite Bacon 151",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "63337014",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public void Should_Not_Create_New_Book_When_Book_With_Isbn_Already_Exist_With_Status_Inactive()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Rustic Unbranded Fresh Fish 880",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "22289594",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.CreateBook(expectedBook));
        }

        [Test]
        public async Task Should_Create_New_Book_When_Book_With_Title_Already_Exist_With_Status_Deleted()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Rustic Unbranded Wooden Hat 391",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "63337014",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };

            // act
            var actualBook = await booksDataService.CreateBook(expectedBook);

            // assert
            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Quantity, actualBook.Quantity);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.AreEqual(Status.Active.ToString(), actualBook.Status);
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Author.Description, actualBook.Author.Description);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
        }

        [Test]
        public async Task Should_Create_New_Book_When_Book_With_Isbn_Already_Exist_With_Status_Deleted()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "935CECC7-8ED5-4D79-9AE2-B39287E8BE09",
                Title = "Intelligent Unbranded Concrete Computer 320",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "45611258",
                Description = "Quibusdectus magnam nihil consequuntur veritatis voluptas amet esse voluptate praesentium ratione ullam quia.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = "Active",
                Author =
                    new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Id = 4,
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        FirstName = "Princess",
                        LastName = "Hessel",
                        Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        CreatedBy = "System",
                        ModifiedDate = null,
                        ModifiedBy = null,
                        Status = "Active"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Id = 15,
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = "Active"
                }
            };

            // act
            var actualBook = await booksDataService.CreateBook(expectedBook);

            // assert
            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Quantity, actualBook.Quantity);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.AreEqual(Status.Active.ToString(), actualBook.Status);
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Author.Description, actualBook.Author.Description);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
        }

        [Test]
        public async Task Should_Return_Books_With_Default_Order()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedTotalCount = _data.Count(x => !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));
            var expectedItemsCount = _data.Count(x => !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));
            var expectedFirstItemId = 1;

            // act
            var book = await booksDataService.GetBooks(null, null, String.Empty, String.Empty);

            // assert
            Assert.NotNull(book.TotalCount);
            Assert.NotNull(book.Items);
            Assert.AreEqual(expectedTotalCount, book.TotalCount);
            Assert.AreEqual(expectedItemsCount, book.Items.Length);
            Assert.AreEqual(expectedFirstItemId, book.Items[0].Id);
        }

        [TestCase("title_asc", 9)]
        [TestCase("publisher_asc", 5)]
        [TestCase("language_asc", 3)]
        [TestCase("isbn_asc", 3)]
        [TestCase("description_asc", 30)]
        [TestCase("quantity_asc", 5)]
        [TestCase("paperback_asc", 29)]
        [TestCase("publishdate_asc", 10)]
        [TestCase("createddate_asc", 2)]
        [TestCase("modifieddate_asc", 2)]
        [TestCase("title_desc", 7)]
        [TestCase("publisher_desc", 11)]
        [TestCase("language_desc", 1)]
        [TestCase("isbn_desc", 9)]
        [TestCase("description_desc", 11)]
        [TestCase("quantity_desc", 1)]
        [TestCase("paperback_desc", 3)]
        [TestCase("publishdate_desc", 2)]
        [TestCase("createddate_desc", 3)]
        [TestCase("modifieddate_desc", 3)]
        public async Task Should_Return_Books_With_Specified_Order(string order, long expectedFirstItemId)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);

            // act
            var book = await booksDataService.GetBooks(null, null, order, String.Empty);

            // assert
            Assert.NotNull(book.TotalCount);
            Assert.NotNull(book.Items);
            Assert.AreEqual(expectedFirstItemId, book.Items[0].Id);
        }


        [TestCase("title_asc", 10)]
        [TestCase("publisher_asc", 6)]
        [TestCase("language_asc", 3)]
        [TestCase("isbn_asc", 3)]
        [TestCase("description_asc", 6)]
        [TestCase("quantity_asc", 6)]
        [TestCase("paperback_asc", 6)]
        [TestCase("publishdate_asc", 10)]
        [TestCase("createddate_asc", 10)]
        [TestCase("modifieddate_asc", 10)]
        [TestCase("title_desc", 3)]
        [TestCase("publisher_desc", 10)]
        [TestCase("language_desc", 6)]
        [TestCase("isbn_desc", 6)]
        [TestCase("description_desc", 3)]
        [TestCase("quantity_desc", 3)]
        [TestCase("paperback_desc", 3)]
        [TestCase("publishdate_desc", 6)]
        [TestCase("createddate_desc", 3)]
        [TestCase("modifieddate_desc", 3)]
        public async Task Should_Return_Books_With_Specified_Order_Which_Contains_Text(string order, long expectedFirstItemId)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedTotalCount = 3;
            var expectedItemsCount = 3;
            var searchString = "Small";

            // act
            var book = await booksDataService.GetBooks(null, null, order, searchString);

            // assert
            Assert.NotNull(book.TotalCount);
            Assert.NotNull(book.Items);
            Assert.AreEqual(expectedTotalCount, book.TotalCount);
            Assert.AreEqual(expectedItemsCount, book.Items.Length);
            Assert.AreEqual(expectedFirstItemId, book.Items[0].Id);
        }

        [TestCase(1,0,1)]
        [TestCase(1, 1, 2)]
        [TestCase(1, 2, 3)]
        [TestCase(1, 3, 4)]
        [TestCase(1, 4, 5)]
        [TestCase(1, 5, 6)]
        [TestCase(1, 6, 7)]
        [TestCase(1, 7, 8)]
        [TestCase(1, 8, 9)]
        [TestCase(1, 9, 10)]
        public async Task Should_Return_Books_With_Offset_And_Count(int count, int offset, int expectedFirstItemId)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedTotalCount = 13;
            var expectedItemsCount = 1;

            // act
            var book = await booksDataService.GetBooks(count, offset, String.Empty, String.Empty);

            // assert
            Assert.NotNull(book.TotalCount);
            Assert.NotNull(book.Items);
            Assert.AreEqual(expectedTotalCount, book.TotalCount);
            Assert.AreEqual(expectedItemsCount, book.Items.Length);
            Assert.AreEqual(expectedFirstItemId, book.Items[0].Id);
        }

        [Test]
        public async Task Should_Update_Book()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "6A0734F0-B3DE-44D6-93E3-F9E504A35A6E",
                Title = "Small Unbranded Cotton Tuna 258",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "96333500",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = "Test",
                Status = "Inactive",
                Author = new ClawLibrary.Core.Models.Authors.Author()
                    {
                        Key = "E9862AF8-0017-4C36-B424-68E2D39D10DF", FirstName = "Arvel", LastName = "Greenfelder"
                    },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Key = "E7F716C2-FA5E-433C-A3C3-478F1B94E44B", Name = "Guide"
                }
            };

            // act
            var actualBook = await booksDataService.UpdateBook(expectedBook);

            // assert
            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Quantity, actualBook.Quantity);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.NotNull(actualBook.ModifiedDate);
            Assert.AreEqual(DateTimeOffset.Now.Date, actualBook.ModifiedDate.Value.Date);
            Assert.AreEqual(expectedBook.ModifiedBy, actualBook.ModifiedBy);
            Assert.AreEqual(expectedBook.Status, actualBook.Status);
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
        }

        [TestCase("wrongBookKey")]
        [TestCase("    ")]
        [TestCase("123123213")]
        [TestCase("")]
        [TestCase(null)]
        public void Should_Not_Update_Book_And_Should_Throw_Exception_When_Book_Key_Is_Null(string bookKey)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = bookKey,
                Title = "Small Unbranded Cotton Tuna 258",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "96333500",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = "Test",
                Status = "Inactive",
                Author = new ClawLibrary.Core.Models.Authors.Author()
                {
                    Key = "E9862AF8-0017-4C36-B424-68E2D39D10DF",
                    FirstName = "Arvel",
                    LastName = "Greenfelder"
                },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Key = "E7F716C2-FA5E-433C-A3C3-478F1B94E44B",
                    Name = "Guide"
                }
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.UpdateBook(expectedBook));
        }

        [TestCase("wrongKey")]
        [TestCase("    ")]
        [TestCase("123123213")]
        [TestCase("")]
        [TestCase(null)]
        public void Should_Not_Update_Book_And_Should_Throw_Exception_When_Author_Key_Is_Null(string authorKey)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "6A0734F0-B3DE-44D6-93E3-F9E504A35A6E",
                Title = "Small Unbranded Cotton Tuna 258",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "96333500",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = "Test",
                Status = "Inactive",
                Author = new ClawLibrary.Core.Models.Authors.Author()
                {
                    Key = authorKey,
                    FirstName = "Arvel",
                    LastName = "Greenfelder"
                },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Key = "E7F716C2-FA5E-433C-A3C3-478F1B94E44B",
                    Name = "Guide"
                }
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.UpdateBook(expectedBook));
        }

        [TestCase("wrongKey")]
        [TestCase("    ")]
        [TestCase("123123213")]
        [TestCase("")]
        [TestCase(null)]
        public void Should_Not_Update_Book_And_Should_Throw_Exception_When_Category_Key_Is_Null(string categoryKey)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "6A0734F0-B3DE-44D6-93E3-F9E504A35A6E",
                Title = "Small Unbranded Cotton Tuna 258",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "96333500",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = "Test",
                Status = "Inactive",
                Author = new ClawLibrary.Core.Models.Authors.Author()
                {
                    Key = "E9862AF8-0017-4C36-B424-68E2D39D10DF",
                    FirstName = "Arvel",
                    LastName = "Greenfelder"
                },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Key = categoryKey,
                    Name = "Guide"
                }
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.UpdateBook(expectedBook));
        }

        [TestCase("wrongStatus")]
        [TestCase("    ")]
        [TestCase("a123123213")]
        [TestCase("")]
        [TestCase(null)]
        public void Should_Not_Update_Book_And_Should_Throw_Exception_When_Status_Is_Wrong(string status)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBook = new Core.Models.Books.Book()
            {
                Id = 15,
                Key = "6A0734F0-B3DE-44D6-93E3-F9E504A35A6E",
                Title = "Small Unbranded Cotton Tuna 258",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "96333500",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Quantity = 52,
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = "Test",
                Status = status,
                Author = new ClawLibrary.Core.Models.Authors.Author()
                {
                    Key = "E9862AF8-0017-4C36-B424-68E2D39D10DF",
                    FirstName = "Arvel",
                    LastName = "Greenfelder"
                },
                Category = new ClawLibrary.Core.Models.Categories.Category
                {
                    Key = "E7F716C2-FA5E-433C-A3C3-478F1B94E44B",
                    Name = "Guide"
                }
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.UpdateBook(expectedBook));
        }

        [Test]
        public async Task Should_Create_New_Book_Picture()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            _context.ResetValueGenerators();
            var expectedBookKey = "6A0734F0-B3DE-44D6-93E3-F9E504A35A6E";
            var expectedFile = new File()
            {
                FileName = "test2FileName",
                CreatedBy = "Test",
                CreatedDate = DateTimeOffset.Now,
                Status = Status.Active.ToString()
            };

            // act
            var isFileExisting =
                await _context.File.AnyAsync(
                    x => x.FileName.ToLower().Equals(expectedFile.FileName.ToLower()));

            await booksDataService.UpdatePicture(expectedFile.FileName, expectedBookKey, expectedFile.CreatedBy);

            var actualFile =  await _context.File.FirstOrDefaultAsync(x => x.FileName.ToLower().Equals(expectedFile.FileName.ToLower()));

            var book =
                await _context.Book.Include("ImageFile").FirstOrDefaultAsync(
                    x => x.Key.ToString().ToLower().Equals(expectedBookKey.ToLower()));

            // assert
            Assert.NotNull(actualFile);
            Assert.False(isFileExisting);
            Assert.NotNull(book.ImageFile);
            Assert.AreEqual(expectedFile.FileName, actualFile.FileName);
            Assert.AreEqual(expectedFile.CreatedBy, actualFile.CreatedBy);
            Assert.AreEqual(expectedFile.CreatedDate.Date, actualFile.CreatedDate.Date);
            Assert.AreEqual(expectedFile.Status, actualFile.Status);

        }

        [Test]
        public async Task Should_Create_Update_Book_Picture()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBookKey = "D1BBA9A4-5613-480C-9424-FDB4933D2E82";
            var expectedFile = new File()
            {
                FileName = "test2FileName",
                ModifiedBy = "Test",
                ModifiedDate = DateTimeOffset.Now,
                Status = Status.Active.ToString()
            };


            // act
            var isFileExisting =
                await _context.File.AnyAsync(
                    x => x.FileName.ToLower().Equals(expectedFile.FileName.ToLower()));

            var oldFileName = _context.Book.Include("ImageFile").FirstOrDefault(
                x => x.Key.ToString().ToLower().Equals(expectedBookKey.ToLower()))
                ?.ImageFile.FileName;


            await booksDataService.UpdatePicture(expectedFile.FileName, expectedBookKey, expectedFile.ModifiedBy);

            var actualFile = await _context.File.FirstOrDefaultAsync(x => x.FileName.ToLower().Equals(expectedFile.FileName.ToLower()));

            var book =
                await _context.Book.Include("ImageFile").FirstOrDefaultAsync(
                    x => x.Key.ToString().ToLower().Equals(expectedBookKey.ToLower()));

            // assert
            Assert.NotNull(actualFile);
            Assert.False(String.IsNullOrEmpty(oldFileName));
            Assert.AreNotEqual(oldFileName, actualFile.FileName);
            Assert.False(isFileExisting);
            Assert.NotNull(book.ImageFile);
            Assert.AreEqual(expectedFile.FileName, actualFile.FileName);
            Assert.AreEqual(expectedFile.ModifiedBy, actualFile.ModifiedBy);
            Assert.AreEqual(expectedFile.ModifiedDate?.Date, actualFile.ModifiedDate?.Date);
            Assert.AreEqual(expectedFile.Status, actualFile.Status);
        }

        [TestCase("wrongKey")]
        [TestCase("    ")]
        [TestCase("a123123213")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("2E6C91BE-A96F-4F72-9644-90D0EEC7DEE3")]
        public void Should_Not_Update_Book_Picture_And_Should_Throw_Exception_When_Book_Key_Is_Wrong(string expectedBookKey)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.UpdatePicture(String.Empty, expectedBookKey, String.Empty));
        }


        [TestCase("wrongKey")]
        [TestCase("    ")]
        [TestCase("a123123213")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("2E6C91BE-A96F-4F72-9644-90D0EEC7DEE3")]
        public void Should_Not_Return_Book_Picture_And_Should_Throw_Exception_When_Book_Key_Is_Wrong(string expectedBookKey)
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await booksDataService.GetFileNameOfBookPicture(expectedBookKey));
        }

        [Test]
        public async Task Should_Return_File_Name_Of_Book_Picture()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBookKey = "D1BBA9A4-5613-480C-9424-FDB4933D2E82";
            var expectedFile = new File()
            {
                FileName = "testFileName",
                Key = Guid.NewGuid(),
                Id = 1,
                CreatedBy = "System",
                CreatedDate = DateTimeOffset.Now,
                Status = Status.Active.ToString()
            };

            // act
            var actualFileName = await booksDataService.GetFileNameOfBookPicture(expectedBookKey);
            
            // assert
            Assert.NotNull(actualFileName);
            Assert.IsNotEmpty(actualFileName);
            Assert.AreEqual(expectedFile.FileName, actualFileName);
        }

        [Test]
        public async Task Should_Return_Empty_String_When_Book_Picture_Does_Not_Exist()
        {
            // arrange
            var booksDataService = new BooksDataService(_mapper, _context);
            var expectedBookKey = "6A0734F0-B3DE-44D6-93E3-F9E504A35A6E";

            // act
            var actualFileName = await booksDataService.GetFileNameOfBookPicture(expectedBookKey);

            // assert
            Assert.AreEqual(String.Empty, actualFileName);
        }

    }
}
