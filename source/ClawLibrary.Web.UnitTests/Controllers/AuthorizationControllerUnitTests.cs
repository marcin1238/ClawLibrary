using System;
using System.Threading.Tasks;
using ClawLibrary.Core.Models.Auth;
using ClawLibrary.Services.ApiServices;
using ClawLibrary.Services.Models.Users;
using ClawLibrary.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ClawLibrary.Web.UnitTests.Controllers
{
    [TestFixture]
    public class AuthorizationControllerUnitTests
    {
        private Mock<IAuthApiService> _authService;

        [SetUp]
        protected void SetUp()
        {
            _authService = new Mock<IAuthApiService>();
        }

        [Test]
        public async Task Should_Authorize_User()
        {
            // arrange
            var request = new AuthorizeRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = "test"
            };
            var expectedToken=new Token()
            {
                AccessToken = "eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0.eyJzdWIiOiJEYXZpZC5TY2h3aW1tZXJAdGVzdC5jb20iLCJqdGkiOiJjYTY3NjJkOC1hZGVkLTQ1YWEtOWZkNS04NTE1NDBjYjZhMWQiLCJpYXQiOiI4NjM5OS45OTkwMDA5IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkRhdmlkLlNjaHdpbW1lckB0ZXN0LmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJBZG1pbiIsIlJlZ3VsYXIiXSwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiOTk1NUM1Q0EtOEJBQS00QzY5LThFOUEtQUExN0FFNTExMzhFIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9ncm91cHNpZCI6IkRhdmlkLlNjaHdpbW1lckB0ZXN0LmNvbSIsIm5iZiI6MTUxMTI2MjE3MCwiZXhwIjoxNTExMzQ4NTcwfQ.",
                ExpiresAt = new DateTimeOffset(new DateTime(2017,11,22,12,05,10)),
                ExpiresIn = 86399,
                TokenType = "Bearer"
            };
            _authService.Setup(x => x.Authorize(It.IsAny<AuthorizeRequest>()))
                .Returns(Task.FromResult<Token>(expectedToken));

            var controller = new AuthorizationController(_authService.Object);

            // act
            var result = await controller.Post(request);

            // assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var actual = (ObjectResult) result;
            Assert.AreEqual(expectedToken.ToString(), actual.Value.ToString());
        }

        [Test]
        public async Task Should_Return_Unauthorized_When_Authorize_Api_Service_Return_Null()
        {
            // arrange
            var request = new AuthorizeRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = "test"
            };
            _authService.Setup(x => x.Authorize(It.IsAny<AuthorizeRequest>()))
                .Returns(Task.FromResult<Token>(null));

            var controller = new AuthorizationController(_authService.Object);

            // act
            var result = await controller.Post(request);

            // assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public async Task Should_Verify_User()
        {
            // arrange
            var request = new UserVerificationRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = "test",
                VerificationCode = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E"
            };
            var expectedToken = new Token()
            {
                AccessToken = "eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0.eyJzdWIiOiJEYXZpZC5TY2h3aW1tZXJAdGVzdC5jb20iLCJqdGkiOiJjYTY3NjJkOC1hZGVkLTQ1YWEtOWZkNS04NTE1NDBjYjZhMWQiLCJpYXQiOiI4NjM5OS45OTkwMDA5IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkRhdmlkLlNjaHdpbW1lckB0ZXN0LmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJBZG1pbiIsIlJlZ3VsYXIiXSwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiOTk1NUM1Q0EtOEJBQS00QzY5LThFOUEtQUExN0FFNTExMzhFIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9ncm91cHNpZCI6IkRhdmlkLlNjaHdpbW1lckB0ZXN0LmNvbSIsIm5iZiI6MTUxMTI2MjE3MCwiZXhwIjoxNTExMzQ4NTcwfQ.",
                ExpiresAt = new DateTimeOffset(new DateTime(2017, 11, 22, 12, 05, 10)),
                ExpiresIn = 86399,
                TokenType = "Bearer"
            };
            _authService.Setup(x => x.VerifyUser(It.IsAny<UserVerificationRequest>()))
                .Returns(Task.FromResult<Token>(expectedToken));

            var controller = new AuthorizationController(_authService.Object);

            // act
            var result = await controller.Post(request);

            // assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var actual = (ObjectResult)result;
            Assert.AreEqual(expectedToken.ToString(), actual.Value.ToString());
        }

        [Test]
        public async Task Should_Return_Unauthorized_When_Verify_User_Api_Service_Return_Null()
        {
            // arrange
            var request = new UserVerificationRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = "test",
                VerificationCode = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E"
            };
            _authService.Setup(x => x.VerifyUser(It.IsAny<UserVerificationRequest>()))
                .Returns(Task.FromResult<Token>(null));

            var controller = new AuthorizationController(_authService.Object);

            // act
            var result = await controller.Post(request);

            // assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public async Task Should_Reset_Password()
        {
            // arrange
            var request = new ResetUserPasswordRequest()
            {
                Email = "David.Schwimmer@test.com"
            };
            var controller = new AuthorizationController(_authService.Object);

            // act
            var result = await controller.Post(request);

            // assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Should_Set_Password()
        {
            // arrange
            var request = new SetUserPasswordRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = "test",
                VerificationCode = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                ConfirmPassword = "test"
            };
            var controller = new AuthorizationController(_authService.Object);

            // act
            var result = await controller.Post(request);

            // assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Should_Register_User()
        {
            // arrange
            var request = new RegisterUserRequest()
            {
                Email = "David.Schwimmer@test.com",
                Password = "test",
                FirstName = "David",
                LastName = "Schwimmer",
                ConfirmPassword = "test"
            };
            var controller = new AuthorizationController(_authService.Object);

            // act
            var result = await controller.Post(request);

            // assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}
