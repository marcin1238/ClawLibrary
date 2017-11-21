using System;
using System.Threading.Tasks;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Models;
using ClawLibrary.Services.ApiServices;
using ClawLibrary.Services.Models;
using ClawLibrary.Services.Models.Authors;
using ClawLibrary.Services.Models.Books;
using ClawLibrary.Services.Models.Categories;
using ClawLibrary.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ClawLibrary.Web.UnitTests.Controllers
{
    [TestFixture]
    public class BooksControllerUnitTests
    {
        private Mock<IBooksApiService> _apiService;

        [SetUp]
        protected void SetUp()
        {
            _apiService = new Mock<IBooksApiService>();
        }

        [Test]
        public async Task Should_Return_Book_With_Specified_Key()
        {
            // arrange
            var bookKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF";
            var expectedBook = new BookResponse()
            {
                Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                Title = "Small Unbranded Cotton Tuna 258",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "96333500",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                ModifiedDate = null,
                Status = Status.Active,
                Author = new AuthorResponse()
                {
                    Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                    FirstName = "Princess",
                    LastName = "Hessel",
                    Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    ModifiedDate = null,
                    Status = Status.Active,
                },
                Category = new CategoryResponse()
                {
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    Status = Status.Active,
                }
            };
            _apiService.Setup(x => x.GetBookByKey(It.IsAny<string>()))
                .Returns(Task.FromResult<BookResponse>(expectedBook));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Get(bookKey);

            // assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var actual = (ObjectResult)result;
            Assert.AreEqual(expectedBook, actual.Value);
        }

        [Test]
        public async Task Should_Not_Return_Book_When_Book_Does_Not_Exist()
        {
            // arrange
            var bookKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF";
            _apiService.Setup(x => x.GetBookByKey(It.IsAny<string>()))
                .Returns(Task.FromResult<BookResponse>(null));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Get(bookKey);

            // assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Should_Return_Books()
        {
            // arrange
            var expectedBooks = new ListResponse<BookResponse>()
            {
                Items = new BookResponse[]
                {
                    new BookResponse()
                    {
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        Title = "Small Unbranded Cotton Tuna 258",
                        Publisher = "Ward and Sons",
                        Language = "DE",
                        Isbn = "96333500",
                        Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                        Paperback = 620,
                        PublishDate = new DateTime(1959, 2, 17),
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        ModifiedDate = null,
                        Status = Status.Active,
                        Author = new AuthorResponse()
                        {
                            Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                            FirstName = "Princess",
                            LastName = "Hessel",
                            Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                            CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                            ModifiedDate = null,
                            Status = Status.Active,
                        },
                        Category = new CategoryResponse()
                        {
                            Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                            Name = "History",
                            CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                            Status = Status.Active,
                        }
                    },
                    new BookResponse()
                    {
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        Title = "Small Unbranded Cotton Tuna 258",
                        Publisher = "Ward and Sons",
                        Language = "DE",
                        Isbn = "96333500",
                        Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                        Paperback = 620,
                        PublishDate = new DateTime(1959, 2, 17),
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        ModifiedDate = null,
                        Status = Status.Active,
                        Author = new AuthorResponse()
                        {
                            Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                            FirstName = "Princess",
                            LastName = "Hessel",
                            Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                            CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                            ModifiedDate = null,
                            Status = Status.Active,
                        },
                        Category = new CategoryResponse()
                        {
                            Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                            Name = "History",
                            CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                            Status = Status.Active,
                        }
                    },
                    new BookResponse()
                    {
                        Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                        Title = "Small Unbranded Cotton Tuna 258",
                        Publisher = "Ward and Sons",
                        Language = "DE",
                        Isbn = "96333500",
                        Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                        Paperback = 620,
                        PublishDate = new DateTime(1959, 2, 17),
                        CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                        ModifiedDate = null,
                        Status = Status.Active,
                        Author = new AuthorResponse()
                        {
                            Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                            FirstName = "Princess",
                            LastName = "Hessel",
                            Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                            CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                            ModifiedDate = null,
                            Status = Status.Active,
                        },
                        Category = new CategoryResponse()
                        {
                            Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                            Name = "History",
                            CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                            Status = Status.Active,
                        }
                    }
                },
                TotalCount = 3
            };
            _apiService.Setup(x => x.GetBooks(It.IsAny<QueryData>()))
                .Returns(Task.FromResult<ListResponse<BookResponse>>(expectedBooks));

            // act
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Get(new QueryData());

            // assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var actual = (ObjectResult)result;
            var actualValue = (ListResponse<BookResponse>) actual.Value;
            Assert.AreEqual(expectedBooks.Items, actualValue.Items);
            Assert.AreEqual(expectedBooks.TotalCount, actualValue.TotalCount);
        }

        [Test]
        public async Task Should_Not_Return_Books_When_Books_Does_Not_Exist()
        {
            // arrange
            _apiService.Setup(x => x.GetBooks(It.IsAny<QueryData>()))
                .Returns(Task.FromResult<ListResponse<BookResponse>>(null));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Get(new QueryData());

            // assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Should_Create_New_Book()
        {
            // arrange
            var expectedBook = new BookResponse()
            {
                Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                Title = "Small Unbranded Cotton Tuna 258",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "96333500",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                ModifiedDate = null,
                Status = Status.Active,
                Author = new AuthorResponse()
                {
                    Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                    FirstName = "Princess",
                    LastName = "Hessel",
                    Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    ModifiedDate = null,
                    Status = Status.Active,
                },
                Category = new CategoryResponse()
                {
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    Status = Status.Active,
                }
            };
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
            _apiService.Setup(x => x.CreateBook(It.IsAny<BookRequest>()))
                .Returns(Task.FromResult<BookResponse>(expectedBook));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Post(request);

            // assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var actual = (ObjectResult)result;
            Assert.AreEqual(expectedBook, actual.Value);
        }

        [Test]
        public async Task Should_Return_Bad_Request_Result_When_Create_Book_Api_Service_Return_Null()
        {
            // arrange
            var request = new BookRequest();
            _apiService.Setup(x => x.CreateBook(It.IsAny<BookRequest>()))
                .Returns(Task.FromResult<BookResponse>(null));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Post(request);

            // assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }


        [Test]
        public async Task Should_Update_Book_With_Specified_Key()
        {
            // arrange
            var bookKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF";
            var expectedBook = new BookResponse()
            {
                Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                Title = "Small Unbranded Cotton Tuna 258",
                Publisher = "Ward and Sons",
                Language = "DE",
                Isbn = "96333500",
                Description =
                    "Deleniti qui quos nihil molestiae ea nulla voluptatibus aperiam soluta est voluptatem praesentium nemo illum est delectus corrupti quos sequi et dolore animi enim aliquam in quasi et minima hic ut vel doloribus minus quia iure occaecati et animi sint aliquid aliquid eligendi voluptate veniam aut suscipit aliquid dolores distinctio voluptate nemo animi sapiente et ut voluptatibus modi ducimus pariatur nostrum animi cumque vitae quo fugiat enim est amet molestiae quia quisquam rerum sunt aut ducimus iste adipisci doloribus sequi et porro placeat dolorum perspiciatis distinctio numquam doloremque odit necessitatibus ea exercitationem autem facere et placeat est est optio ducimus eum est dolorem quasi qui nobis quam doloremque nesciunt sit cum temporibus incidunt exercitationem id sunt at vitae ut ab dolorum et dolorum exercitationem voluptatem voluptates molestiae dolores earum illo quibusdam rerum dolores perspiciatis quia harum omnis cum dolorem vero et sit accusamus molestiae non et et et ratione inventore saepe similique quasi facilis eum qui doloribus accusamus quae perspiciatis possimus veniam sunt aut eum minus nostrum est aut aut sit quia sint soluta aut iusto quis eligendi dicta voluptate voluptas molestiae doloribus aut nesciunt incidunt officiis pariatur porro id rerum soluta harum fuga aut a explicabo assumenda ut deleniti voluptas autem non ipsum eaque porro est reprehenderit voluptas reprehenderit non exercitationem dolor rerum cumque sit exercitationem quia earum sequi vero odio aut ut sit suscipit perspiciatis exercitationem culpa pariatur nisi id sequi est beatae totam ut illum nulla odio culpa atque aut quia cupiditate ut ut asperiores et assumenda nam necessitatibus dolorem iure dolorum rerum.",
                Paperback = 620,
                PublishDate = new DateTime(1959, 2, 17),
                CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                ModifiedDate = null,
                Status = Status.Active,
                Author = new AuthorResponse()
                {
                    Key = "2E218BD7-D56D-428E-8765-64C3825D3EEF",
                    FirstName = "Princess",
                    LastName = "Hessel",
                    Description = "Ea excepturi iae consequatur ut aut debitis empora quasi sequi illo quibusdam.",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    ModifiedDate = null,
                    Status = Status.Active,
                },
                Category = new CategoryResponse()
                {
                    Key = "0E63BE9D-37C9-4C38-86FC-68A85593CD93",
                    Name = "History",
                    CreatedDate = new DateTimeOffset(new DateTime(2017, 9, 11)),
                    Status = Status.Active,
                }
            };
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
            _apiService.Setup(x => x.UpdateBook(It.IsAny<string>(),It.IsAny<BookRequest>()))
                .Returns(Task.FromResult<BookResponse>(expectedBook));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Put(bookKey,request);

            // assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var actual = (ObjectResult)result;
            Assert.AreEqual(expectedBook, actual.Value);
        }


        [Test]
        public async Task Should_Return_Bad_Request_Result_When_Api_Service_Return_Null()
        {
            // arrange
            var request = new BookRequest();
            var bookKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF";
            _apiService.Setup(x => x.UpdateBook(It.IsAny<string>(),It.IsAny<BookRequest>()))
                .Returns(Task.FromResult<BookResponse>(null));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Put(bookKey, request);

            // assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Should_Update_Book_Picture_With_Specified_Key()
        {
            // arrange
            var bookKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF";
            var request = new UpdatePictureRequest()
            {
                PictureBase64 = "asddddddddddddddddddddddasdas"
            };
            var expectedMedia = new Media()
            {
                Content = new byte[]{10,10,10},
                FileName = "test",
                ContentType = "jpg",
                ContentSize = 10,
                Id = "test"
            };
            _apiService.Setup(x => x.UpdatePicture(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult<Media>(expectedMedia));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Patch(bookKey, request);

            // assert
            Assert.IsInstanceOf<FileContentResult>(result);
        }

        [Test]
        public async Task Should_Return_Bad_Request_Result_When_Update_Picture_Api_Service_Return_Null()
        {
            // arrange
            var bookKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF";
            var request = new UpdatePictureRequest()
            {
                PictureBase64 = "asddddddddddddddddddddddasdas"
            };
           
            _apiService.Setup(x => x.UpdatePicture(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult<Media>(null));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Patch(bookKey, request);

            // assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Should_Return_Book_Picture_With_Specified_Key()
        {
            // arrange
            var bookKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF";
           
            var expectedMedia = new Media()
            {
                Content = new byte[] { 10, 10, 10 },
                FileName = "test",
                ContentType = "jpg",
                ContentSize = 10,
                Id = "test"
            };
            _apiService.Setup(x => x.GetPicture(It.IsAny<string>()))
                .Returns(Task.FromResult<Media>(expectedMedia));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Picture(bookKey);

            // assert
            Assert.IsInstanceOf<FileContentResult>(result);
        }
        [Test]
        public async Task Should_Return_Bad_Request_Result_When_Get_Picture_Api_Service_Return_Null()
        {
            // arrange
            var bookKey = "2E218BD7-D56D-428E-8765-64C3825D3EEF";
          
            _apiService.Setup(x => x.GetPicture(It.IsAny<string>()))
                .Returns(Task.FromResult<Media>(null));
            var controller = new BooksController(_apiService.Object);

            // act
            var result = await controller.Picture(bookKey);

            // assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
