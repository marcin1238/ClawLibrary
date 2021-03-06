﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.MediaStorage;
using ClawLibrary.Core.Models;
using ClawLibrary.Core.Services;
using ClawLibrary.Services.ApiServices;
using ClawLibrary.Services.Mapping;
using ClawLibrary.Services.Models.Books;
using ClawLibrary.Services.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ClawLibrary.Services.UnitTests.ApiServicesTests
{
    [TestFixture]
    public class BooksApiServiceUnitTests
    {
        private Mock<IBooksDataService> _dataService;
        private Mock<IMediaStorageAppService> _mediaStorageAppService;
        private IMapper _mapper;
        private Mock<ILogger<BooksApiService>> _logger;
        private Mock<ISessionContextProvider> _provider;

        [SetUp]
        protected void SetUp()
        {
            _dataService = new Mock<IBooksDataService>();
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }));
            _logger = new Mock<ILogger<BooksApiService>>();
            _provider = new Mock<ISessionContextProvider>();
            _mediaStorageAppService = new Mock<IMediaStorageAppService>();
        }

        [Test]
        public async Task Should_Return_Book_With_Specified_Key()
        {
            // arrange
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
            _dataService.Setup(x => x.GetBookByKey(It.IsAny<string>())).Returns(Task.FromResult<Core.Models.Books.Book>(expectedBook));
            var apiService = new BooksApiService(_provider.Object,_dataService.Object,_mediaStorageAppService.Object, _logger.Object, _mapper);

            // act
            var actualBook = await apiService.GetBookByKey(expectedBook.Key);

            // assert
            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.AreEqual(expectedBook.Status, actualBook.Status.ToString());
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Author.Description, actualBook.Author.Description);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
        }
        
        [Test]
        public void Should_Not_Return_Book_And_Throw_Exception_When_Book_Does_Not_Exist()
        {
            // arrange
            _dataService.Setup(x => x.GetBookByKey(It.IsAny<string>())).Returns(Task.FromResult<Core.Models.Books.Book>(null));
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.GetBookByKey(Guid.NewGuid().ToString()));
        }

        [Test]
        public void Should_Not_Return_Book_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
            _dataService.Setup(x => x.GetBookByKey(It.IsAny<string>())).Throws<BusinessException>();
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.GetBookByKey(Guid.NewGuid().ToString()));
        }

        [Test]
        public async Task Should_Create_New_Book()
        {
            // arrange
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
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() {UserEmail = "test", UserId = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"});
            _dataService.Setup(x => x.CreateBook(It.IsAny<Core.Models.Books.Book>())).Returns(Task.FromResult<Core.Models.Books.Book>(expectedBook));
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);
            var request = new BookRequest()
            {
                Title = expectedBook.Title,
                Publisher = expectedBook.Publisher,
                Language = expectedBook.Language,
                Isbn = expectedBook.Isbn,
                Description = expectedBook.Description,
                Paperback = expectedBook.Paperback,
                PublishDate = expectedBook.PublishDate,
                AuthorKey = expectedBook.Author.Key,
                CategoryKey = expectedBook.Category.Key
            };

            // act
            var actualBook = await apiService.CreateBook(request);

            // assert
            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.AreEqual(expectedBook.Status, actualBook.Status.ToString());
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Author.Description, actualBook.Author.Description);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
        }

        [Test]
        public void Should_Not_Create_New_Book_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await apiService.CreateBook(new BookRequest()));
        }

        [Test]
        public void Should_Not_Create_New_Book_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext)null);
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await apiService.CreateBook(new BookRequest()));
        }

        [Test]
        public void Should_Not_Create_New_Book_And_Throw_Exception_When_Book_Does_Not_Exist()
        {
            // arrange
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
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "E9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            _dataService.Setup(x => x.CreateBook(It.IsAny<Core.Models.Books.Book>())).Returns(Task.FromResult<Core.Models.Books.Book>(null));
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);
            var request = new BookRequest()
            {
                Title = expectedBook.Title,
                Publisher = expectedBook.Publisher,
                Language = expectedBook.Language,
                Isbn = expectedBook.Isbn,
                Description = expectedBook.Description,
                Paperback = expectedBook.Paperback,
                PublishDate = expectedBook.PublishDate,
                AuthorKey = expectedBook.Author.Key,
                CategoryKey = expectedBook.Category.Key
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.CreateBook(request));
        }

        [Test]
        public void Should_Not_Create_New_Book_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
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
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "E9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            var request = new BookRequest()
            {
                Title = expectedBook.Title,
                Publisher = expectedBook.Publisher,
                Language = expectedBook.Language,
                Isbn = expectedBook.Isbn,
                Description = expectedBook.Description,
                Paperback = expectedBook.Paperback,
                PublishDate = expectedBook.PublishDate,
                AuthorKey = expectedBook.Author.Key,
                CategoryKey = expectedBook.Category.Key
            };
            _dataService.Setup(x => x.CreateBook(It.IsAny<Core.Models.Books.Book>())).Throws<BusinessException>();
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.CreateBook(request));
        }

        [Test]
        public async Task Should_Return_Books()
        {
            // arrange
            var expectedBooks = new ListResponse<Core.Models.Books.Book>()
            {
                Items = new Core.Models.Books.Book[]
                {
                    new Core.Models.Books.Book(),
                    new Core.Models.Books.Book(),
                    new Core.Models.Books.Book(),
                    new Core.Models.Books.Book(),
                    new Core.Models.Books.Book()
                },
                TotalCount = 5
            };
            _dataService.Setup(x => x.GetBooks(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<ListResponse<Core.Models.Books.Book>>(expectedBooks));
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);
           
            // act
            var actualBooks = await apiService.GetBooks(new QueryData());

            // assert
            Assert.NotNull(actualBooks);
            Assert.AreEqual(expectedBooks.TotalCount, actualBooks.TotalCount);
        }

        [Test]
        public async Task Should_Update_Book()
        {
            // arrange
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
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "E9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            _dataService.Setup(x => x.UpdateBook(It.IsAny<Core.Models.Books.Book>())).Returns(Task.FromResult<Core.Models.Books.Book>(expectedBook));
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);
            var request = new BookRequest()
            {
                Title = expectedBook.Title,
                Publisher = expectedBook.Publisher,
                Language = expectedBook.Language,
                Isbn = expectedBook.Isbn,
                Description = expectedBook.Description,
                Paperback = expectedBook.Paperback,
                PublishDate = expectedBook.PublishDate,
                AuthorKey = expectedBook.Author.Key,
                CategoryKey = expectedBook.Category.Key
            };

            // act
            var actualBook = await apiService.UpdateBook(expectedBook.Key, request);

            // assert
            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.AreEqual(expectedBook.Status, actualBook.Status.ToString());
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Author.Description, actualBook.Author.Description);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
        }

        [Test]
        public void Should_Not_Update_Book_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await apiService.UpdateBook("E9FCFA44-97D0-4D97-8339-62FD41930A3C", new BookRequest()));
        }

        [Test]
        public void Should_Not_Update_Book_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext)null);
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await apiService.UpdateBook("E9FCFA44-97D0-4D97-8339-62FD41930A3C", new BookRequest()));
        }

        [Test]
        public void Should_Not_Update_Book_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
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
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "E9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            _dataService.Setup(x => x.UpdateBook(It.IsAny<Core.Models.Books.Book>())).Throws<BusinessException>();
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);
            var request = new BookRequest()
            {
                Title = expectedBook.Title,
                Publisher = expectedBook.Publisher,
                Language = expectedBook.Language,
                Isbn = expectedBook.Isbn,
                Description = expectedBook.Description,
                Paperback = expectedBook.Paperback,
                PublishDate = expectedBook.PublishDate,
                AuthorKey = expectedBook.Author.Key,
                CategoryKey = expectedBook.Category.Key
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.UpdateBook(expectedBook.Key,request));
        }

        [Test]
        public async Task Should_Update_Book_Status()
        {
            // arrange
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
            Status eexpectedBookStatus = Status.Active;
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "E9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            _dataService.Setup(x => x.UpdateBookStatus(It.IsAny<string>(), It.IsAny<Status>(), It.IsAny<string>())).Returns(Task.FromResult<Core.Models.Books.Book>(expectedBook));
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);
           
            // act
            var actualBook = await apiService.UpdateBookStatus(expectedBook.Key, eexpectedBookStatus);

            // assert
            Assert.NotNull(actualBook);
            Assert.AreEqual(expectedBook.Title, actualBook.Title);
            Assert.AreEqual(expectedBook.Publisher, actualBook.Publisher);
            Assert.AreEqual(expectedBook.Language, actualBook.Language);
            Assert.AreEqual(expectedBook.Isbn, actualBook.Isbn);
            Assert.AreEqual(expectedBook.Description, actualBook.Description);
            Assert.AreEqual(expectedBook.Paperback, actualBook.Paperback);
            Assert.AreEqual(expectedBook.PublishDate, actualBook.PublishDate);
            Assert.AreEqual(expectedBook.Status, actualBook.Status.ToString());
            Assert.AreEqual(expectedBook.Author.FirstName, actualBook.Author.FirstName);
            Assert.AreEqual(expectedBook.Author.LastName, actualBook.Author.LastName);
            Assert.AreEqual(expectedBook.Author.Description, actualBook.Author.Description);
            Assert.AreEqual(expectedBook.Category.Name, actualBook.Category.Name);
        }

        [Test]
        public void Should_Not_Update_Book_Status_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await apiService.UpdateBookStatus("E9FCFA44-97D0-4D97-8339-62FD41930A3C", Status.Active));
        }

        [Test]
        public void Should_Not_Update_Book_Status_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext)null);
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await apiService.UpdateBookStatus("E9FCFA44-97D0-4D97-8339-62FD41930A3C", Status.Active));
        }

        [Test]
        public void Should_Not_Update_Book_Status_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
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
            Status eexpectedBookStatus = Status.Active;
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "E9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            _dataService.Setup(x => x.UpdateBookStatus(It.IsAny<string>(),It.IsAny<Status>(),It.IsAny<string>())).Throws<BusinessException>();
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);
            var request = new BookRequest()
            {
                Title = expectedBook.Title,
                Publisher = expectedBook.Publisher,
                Language = expectedBook.Language,
                Isbn = expectedBook.Isbn,
                Description = expectedBook.Description,
                Paperback = expectedBook.Paperback,
                PublishDate = expectedBook.PublishDate,
                AuthorKey = expectedBook.Author.Key,
                CategoryKey = expectedBook.Category.Key
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.UpdateBookStatus(expectedBook.Key, eexpectedBookStatus));
        }

        [Test]
        public async Task Should_Update_Book_Picture()
        {
            // arrange
            var stringBase64 = PictureHelper.GetCorrectPictureBase64();
            var image = Convert.FromBase64String(stringBase64);
            Media expectedMedia = new Media()
            {
                Content = image,
                ContentSize = image.Length,
                Id = "E9FCFA44-97D0-4D97-8339-62FD41930A3C",
                FileName = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"
            };
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            _dataService.Setup(x => x.GetFileNameOfBookPicture(It.IsAny<string>())).Returns(Task.FromResult<string>(expectedMedia.FileName));
            _mediaStorageAppService.Setup(x => x.SaveMedia(It.IsAny<byte[]>())).Returns(expectedMedia.FileName);
            _mediaStorageAppService.Setup(x => x.GetMedia(It.IsAny<string>())).Returns(expectedMedia.Content);
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);
            // act
            var actualMedia = await apiService.UpdatePicture(stringBase64, "F9FCFA44-97D0-4D97-8339-62FD41930A3C");

            // assert
            Assert.NotNull(actualMedia);
            Assert.AreEqual(expectedMedia.Content, actualMedia.Content);
            Assert.AreEqual(expectedMedia.ContentSize, actualMedia.ContentSize);
            Assert.AreEqual(expectedMedia.Id, actualMedia.Id);
            Assert.AreEqual(expectedMedia.FileName, actualMedia.FileName);
        }

        [Test]
        public void Should_Not_Update_Book_Picture_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext)null);

            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.UpdatePicture(stringBase64Correct, "F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Update_Book_Picture_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });

            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.UpdatePicture(stringBase64Correct, "F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Update_Book_Picture_When_File_Size_Is_Too_Big()
        {
            // arrange
            var stringBase64 = PictureHelper.GetPictureBase64WithHugeSize();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.UpdatePicture(stringBase64, "F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Update_Book_Picture_When_Image_File_Is_Wrong()
        {
            // arrange
            var stringBase64 = PictureHelper.GetWrongPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.UpdatePicture(stringBase64, "F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Update_Book_Picture_When_Data_Service_Throws_Exception()
        {
            // arrange
            var stringBase64 = PictureHelper.GetCorrectPictureBase64();
            var image = Convert.FromBase64String(stringBase64);
            Media expectedMedia = new Media()
            {
                Content = image,
                ContentSize = image.Length,
                Id = "E9FCFA44-97D0-4D97-8339-62FD41930A3C",
                FileName = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"
            };
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });

            _dataService.Setup(x => x.UpdatePicture(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<BusinessException>();
            _dataService.Setup(x => x.GetFileNameOfBookPicture(It.IsAny<string>())).Returns(Task.FromResult<string>(expectedMedia.FileName));
            _mediaStorageAppService.Setup(x => x.SaveMedia(It.IsAny<byte[]>())).Returns(expectedMedia.FileName);
            _mediaStorageAppService.Setup(x => x.GetMedia(It.IsAny<string>())).Returns(expectedMedia.Content);
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.UpdatePicture(stringBase64, "F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Return_Book_Picture_When_Data_Service_Throws_Exception()
        {
            // arrange
            _dataService.Setup(x => x.GetFileNameOfBookPicture(It.IsAny<string>())).Throws<BusinessException>();
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.GetPicture("F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public async Task Should_Return_Book_Picture()
        {
            // arrange
            var stringBase64 = PictureHelper.GetCorrectPictureBase64();
            var image = Convert.FromBase64String(stringBase64);
            Media expectedMedia = new Media()
            {
                Content = image,
                ContentSize = image.Length,
                Id = "E9FCFA44-97D0-4D97-8339-62FD41930A3C",
                FileName = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"
            };
            _dataService.Setup(x => x.GetFileNameOfBookPicture(It.IsAny<string>())).Returns(Task.FromResult<string>(expectedMedia.FileName));
            _mediaStorageAppService.Setup(x => x.GetMedia(It.IsAny<string>())).Returns(expectedMedia.Content);
            var apiService = new BooksApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _logger.Object, _mapper);
            // act
            var actualMedia = await apiService.GetPicture("F9FCFA44-97D0-4D97-8339-62FD41930A3C");

            // assert
            Assert.NotNull(actualMedia);
            Assert.AreEqual(expectedMedia.Content, actualMedia.Content);
            Assert.AreEqual(expectedMedia.ContentSize, actualMedia.ContentSize);
            Assert.AreEqual(expectedMedia.Id, actualMedia.Id);
            Assert.AreEqual(expectedMedia.FileName, actualMedia.FileName);
        }
    }
}
