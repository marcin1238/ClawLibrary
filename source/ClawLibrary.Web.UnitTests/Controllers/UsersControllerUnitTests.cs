using System;
using System.Threading.Tasks;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Models;
using ClawLibrary.Services.ApiServices;
using ClawLibrary.Services.Models.Authors;
using ClawLibrary.Services.Models.Books;
using ClawLibrary.Services.Models.Categories;
using ClawLibrary.Services.Models.Users;
using ClawLibrary.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ClawLibrary.Web.UnitTests.Controllers
{
    [TestFixture]
    public class UsersControllerUnitTests
    {
        private Mock<IUsersApiService> _apiService;

        [SetUp]
        protected void SetUp()
        {
            _apiService = new Mock<IUsersApiService>();
        }

        [Test]
        public async Task Should_Return_Authenticated_User()
        {
            // arrange
            var expectedUser = new UserResponse()
            {
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "David.Schwimmer@test.com",
                FirstName = "David",
                LastName = "Schwimmer",
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = DateTimeOffset.Now,
                Status = Status.Active
            };
            _apiService.Setup(x => x.GetAuthenticatedUser())
                .Returns(Task.FromResult<UserResponse>(expectedUser));

            var controller = new UsersController(_apiService.Object);

            // act
            var result = await controller.Get();

            // assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var actual = (ObjectResult)result;
            Assert.AreEqual(expectedUser, actual.Value);
        }

        [Test]
        public async Task Should_Not_Return_User_When_User_Is_Not_Authenticated()
        {
            // arrange
           _apiService.Setup(x => x.GetAuthenticatedUser())
                .Returns(Task.FromResult<UserResponse>(null));

            var controller = new UsersController(_apiService.Object);

            // act
            var result = await controller.Get();

            // assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Should_Return_User_With_Specified_Key()
        {
            // arrange
            var expectedKey = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E";
            var expectedUser = new UserResponse()
            {
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "David.Schwimmer@test.com",
                FirstName = "David",
                LastName = "Schwimmer",
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = DateTimeOffset.Now,
                Status = Status.Active
            };
            _apiService.Setup(x => x.GetUserByKey(It.IsAny<string>()))
                .Returns(Task.FromResult<UserResponse>(expectedUser));

            var controller = new UsersController(_apiService.Object);

            // act
            var result = await controller.Get(expectedKey);

            // assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var actual = (ObjectResult)result;
            Assert.AreEqual(expectedUser, actual.Value);
        }

        [Test]
        public async Task Should_Not_Return_User_When_User_Does_Not_Exist()
        {
            // arrange
            var expectedKey = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E";
            _apiService.Setup(x => x.GetAuthenticatedUser())
                .Returns(Task.FromResult<UserResponse>(null));

            var controller = new UsersController(_apiService.Object);

            // act
            var result = await controller.Get(expectedKey);

            // assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Should_Return_Books()
        {
            // arrange
            var expectedUsers = new ListResponse<UserResponse>()
            {
                Items = new UserResponse[]
                {
                    new UserResponse()
                    {
                        Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                        Email = "David.Schwimmer@test.com",
                        FirstName = "David",
                        LastName = "Schwimmer",
                        CreatedDate = DateTimeOffset.Now,
                        ModifiedDate = DateTimeOffset.Now,
                        Status = Status.Active
                    },
                    new UserResponse()
                    {
                        Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                        Email = "David.Schwimmer@test.com",
                        FirstName = "David",
                        LastName = "Schwimmer",
                        CreatedDate = DateTimeOffset.Now,
                        ModifiedDate = DateTimeOffset.Now,
                        Status = Status.Active
                    },
                    new UserResponse()
                    {
                        Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                        Email = "David.Schwimmer@test.com",
                        FirstName = "David",
                        LastName = "Schwimmer",
                        CreatedDate = DateTimeOffset.Now,
                        ModifiedDate = DateTimeOffset.Now,
                        Status = Status.Active
                    }
                },
                TotalCount = 3
            };
            _apiService.Setup(x => x.GetUsers(It.IsAny<QueryData>()))
                .Returns(Task.FromResult<ListResponse<UserResponse>>(expectedUsers));

            // act
            var controller = new UsersController(_apiService.Object);

            // act
            var result = await controller.Get(new QueryData());

            // assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var actual = (ObjectResult)result;
            var actualValue = (ListResponse<UserResponse>)actual.Value;
            Assert.AreEqual(expectedUsers.Items, actualValue.Items);
            Assert.AreEqual(expectedUsers.TotalCount, actualValue.TotalCount);
        }

        [Test]
        public async Task Should_Not_Return_Users_When_Users_Does_Not_Exist()
        {
            // arrange
            _apiService.Setup(x => x.GetUsers(It.IsAny<QueryData>()))
                .Returns(Task.FromResult<ListResponse<UserResponse>>(null));

            // act
            var controller = new UsersController(_apiService.Object);

            // act
            var result = await controller.Get(new QueryData());

            // assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
