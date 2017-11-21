using System;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.MediaStorage;
using ClawLibrary.Core.Models;
using ClawLibrary.Core.Models.Users;
using ClawLibrary.Core.Services;
using ClawLibrary.Services.ApiServices;
using ClawLibrary.Services.Mapping;
using ClawLibrary.Services.Models.Users;
using ClawLibrary.Services.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ClawLibrary.Services.UnitTests.ApiServicesTests
{
    [TestFixture]
    public class UsersApiServiceUnitTests
    {
        private Mock<IUsersDataService> _dataService;
        private Mock<IMediaStorageAppService> _mediaStorageAppService;
        private IMapper _mapper;
        private Mock<ILogger<UsersApiService>> _logger;
        private Mock<ISessionContextProvider> _provider;

        [SetUp]
        protected void SetUp()
        {
            _dataService = new Mock<IUsersDataService>();
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }));
            _logger = new Mock<ILogger<UsersApiService>>();
            _provider = new Mock<ISessionContextProvider>();
            _mediaStorageAppService = new Mock<IMediaStorageAppService>();
        }

        [Test]
        public async Task Should_Return_Authenticated_User()
        {
            // arrange
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "David.Schwimmer@test.com",
                FirstName = "David",
                LastName = "Schwimmer",
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Active.ToString()

            };
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = expectedUser.Email, UserId = expectedUser.Key });
            _dataService.Setup(x => x.GetUserByKey(It.IsAny<string>()))
                .Returns(Task.FromResult<Core.Models.Users.User>(expectedUser));
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act
            var actualUser = await apiService.GetAuthenticatedUser();
            // assert
            Assert.NotNull(actualUser);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.CreatedDate.Date, actualUser.CreatedDate.Date);
            Assert.AreEqual(expectedUser.ModifiedDate?.Date, actualUser.ModifiedDate?.Date);
            Assert.AreEqual(expectedUser.Status.ToLower(), actualUser.Status.ToString().ToLower());
        }

        [Test]
        public void Should_Not_Return_User_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await apiService.GetAuthenticatedUser());
        }

        [Test]
        public void Should_Not_Return_User_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext) null);
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await apiService.GetAuthenticatedUser());
        }

        [Test]
        public void Should_Not_Return_User_And_Throw_Exception_When_Authenticated_User_Does_Not_Exist()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.GetUserByKey(It.IsAny<string>())).Returns(Task.FromResult<Core.Models.Users.User>(null));
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.GetUserByKey(Guid.NewGuid().ToString()));
        }

        [Test]
        public void Should_Not_Return_Authenticated_User_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.GetUserByKey(It.IsAny<string>())).Throws<BusinessException>();
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.GetUserByKey(Guid.NewGuid().ToString()));
        }

        [Test]
        public async Task Should_Return_User_With_Specified_Key()
        {
            // arrange
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "David.Schwimmer@test.com",
                FirstName = "David",
                LastName = "Schwimmer",
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Active.ToString()

            };
            _dataService.Setup(x => x.GetUserByKey(It.IsAny<string>()))
                .Returns(Task.FromResult<Core.Models.Users.User>(expectedUser));
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act
            var actualUser = await apiService.GetUserByKey(expectedUser.Key);
            // assert
            Assert.NotNull(actualUser);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.CreatedDate.Date, actualUser.CreatedDate.Date);
            Assert.AreEqual(expectedUser.ModifiedDate?.Date, actualUser.ModifiedDate?.Date);
            Assert.AreEqual(expectedUser.Status.ToLower(), actualUser.Status.ToString().ToLower());
        }

        [Test]
        public void Should_Not_Return_User_And_Throw_Exception_When_User_Does_Not_Exist()
        {
            // arrange
            _dataService.Setup(x => x.GetUserByKey(It.IsAny<string>())).Returns(Task.FromResult<Core.Models.Users.User>(null));
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.GetUserByKey(Guid.NewGuid().ToString()));
        }

        [Test]
        public void Should_Not_Return_User_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
            _dataService.Setup(x => x.GetUserByKey(It.IsAny<string>())).Throws<BusinessException>();
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.GetUserByKey(Guid.NewGuid().ToString()));
        }

        [Test]
        public async Task Should_Return_Users()
        {
            // arrange
            var expectedUsers = new ListResponse<Core.Models.Users.User>()
            {
                Items = new Core.Models.Users.User[]
                {
                    new Core.Models.Users.User(),
                    new Core.Models.Users.User(),
                    new Core.Models.Users.User(),
                    new Core.Models.Users.User(),
                    new Core.Models.Users.User()
                },
                TotalCount = 5
            };
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.GetUsers(It.IsAny<string>(),It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult<ListResponse<Core.Models.Users.User>>(expectedUsers));
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act
            var actualUsers = await apiService.GetUsers(new QueryData());

            // assert
            Assert.NotNull(actualUsers);
            Assert.AreEqual(actualUsers.TotalCount, actualUsers.TotalCount);
        }

        [Test]
        public void Should_Not_Return_Users_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext) null);
 
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.GetUsers(new QueryData()));
        }

        [Test]
        public void Should_Not_Return_Users_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.GetUsers(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws<BusinessException>();

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.GetUsers(new QueryData()));
        }

        [Test]
        public void Should_Not_Return_Users_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.GetUsers(new QueryData()));
        }

        [Test]
        public async Task Should_Update_User_With_Specified_Key()
        {
            // arrange
            var modifiedByKey = "9855C5CA-8BAA-4C69-8E9A-AA17AE51138E";
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "David.Schwimmer@test.com",
                FirstName = "David",
                LastName = "Schwimmer",
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = DateTimeOffset.Now,
                ModifiedBy = "test@test.pl",
                Status = Status.Active.ToString()

            };
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = modifiedByKey });
            _dataService.Setup(x => x.Update(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult<Core.Models.Users.User>(expectedUser));
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);
            var request = new UserRequest()
            {
                FirstName = expectedUser.FirstName,
                LastName = expectedUser.LastName,
            };

            // act
            var actualUser = await apiService.UpdateUser(request);

            // assert
            Assert.NotNull(actualUser);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.CreatedDate.Date, actualUser.CreatedDate.Date);
            Assert.AreEqual(expectedUser.ModifiedDate?.Date, actualUser.ModifiedDate?.Date);
            Assert.AreEqual(expectedUser.Status.ToLower(), actualUser.Status.ToString().ToLower());
        }

        [Test]
        public void Should_Not_Update_User_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext)null);

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.UpdateUser(new UserRequest()));
        }

        [Test]
        public void Should_Not_Update_User_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.Update(It.IsAny<User>(), It.IsAny<string>()))
                .Throws<BusinessException>();

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.UpdateUser(new UserRequest()));
        }

        [Test]
        public void Should_Not_Update_User_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.UpdateUser(new UserRequest()));
        }

        [Test]
        public async Task Should_Update_User_Status_With_Specified_Key()
        {
            // arrange
            var modifiedByKey = "9855C5CA-8BAA-4C69-8E9A-AA17AE51138E";
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "David.Schwimmer@test.com",
                FirstName = "David",
                LastName = "Schwimmer",
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = DateTimeOffset.Now,
                ModifiedBy = "test@test.pl",
                Status = Status.Active.ToString()

            };
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = modifiedByKey });
            _dataService.Setup(x => x.UpdateStatus(It.IsAny<string>(), It.IsAny<Status>(), It.IsAny<string>()))
                .Returns(Task.FromResult<Core.Models.Users.User>(expectedUser));
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);
            var request = new UserRequest()
            {
                FirstName = expectedUser.FirstName,
                LastName = expectedUser.LastName
            };

            // act
            var actualUser = await apiService.UpdateUserStatus(expectedUser.Key,Status.Active);

            // assert
            Assert.NotNull(actualUser);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.CreatedDate.Date, actualUser.CreatedDate.Date);
            Assert.AreEqual(expectedUser.ModifiedDate?.Date, actualUser.ModifiedDate?.Date);
            Assert.AreEqual(expectedUser.Status.ToLower(), actualUser.Status.ToString().ToLower());
        }

        [Test]
        public void Should_Not_Update_User_Status_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext)null);

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.UpdateUserStatus("9955C5CA-8BAA-4C69-8E9A-AA17AE51138E", Status.Active));
        }

        [Test]
        public void Should_Not_Update_User_Status_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.UpdateStatus(It.IsAny<string>(), It.IsAny<Status>(), It.IsAny<string>()))
                .Throws<BusinessException>();

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.UpdateUserStatus("9955C5CA-8BAA-4C69-8E9A-AA17AE51138E", Status.Active));
        }

        [Test]
        public void Should_Not_Update_User_Status_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.UpdateUserStatus("9955C5CA-8BAA-4C69-8E9A-AA17AE51138E", Status.Active));
        }

        [Test]
        public async Task Should_Update_User_Picture()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            var image = Convert.FromBase64String(stringBase64Correct);
            Media expectedMedia = new Media()
            {
                Content = image,
                ContentSize = image.Length,
                Id = "E9FCFA44-97D0-4D97-8339-62FD41930A3C",
                FileName = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"
            };
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            _dataService.Setup(x => x.GetPicture(It.IsAny<string>())).Returns(Task.FromResult<string>(expectedMedia.FileName));
            _mediaStorageAppService.Setup(x => x.SaveMedia(It.IsAny<byte[]>())).Returns(expectedMedia.FileName);
            _mediaStorageAppService.Setup(x => x.GetMedia(It.IsAny<string>())).Returns(expectedMedia.Content);
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);
            // act
            var actualMedia = await apiService.UpdatePicture(stringBase64Correct, "F9FCFA44-97D0-4D97-8339-62FD41930A3C");

            // assert
            Assert.NotNull(actualMedia);
            Assert.AreEqual(expectedMedia.Content, actualMedia.Content);
            Assert.AreEqual(expectedMedia.ContentSize, actualMedia.ContentSize);
            Assert.AreEqual(expectedMedia.Id, actualMedia.Id);
            Assert.AreEqual(expectedMedia.FileName, actualMedia.FileName);
        }

        [Test]
        public void Should_Not_Update_User_Picture_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext)null);

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.UpdatePicture(stringBase64Correct, "F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Update_User_Picture_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.UpdatePicture(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<BusinessException>();

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.UpdatePicture(stringBase64Correct, "F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Update_User_Picture_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.UpdatePicture(stringBase64Correct, "F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Update_Authenticated_User_Picture_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext)null);

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.UpdatePicture(stringBase64Correct));
        }

        [Test]
        public void Should_Not_Update_Authenticated_User_Picture_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.UpdatePicture(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<BusinessException>();

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.UpdatePicture(stringBase64Correct));
        }

        [Test]
        public void Should_Not_Update_Authenticated_User_Picture_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.UpdatePicture(stringBase64Correct));
        }

        [Test]
        public async Task Should_Update_Authenticated_User_Picture()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            var image = Convert.FromBase64String(stringBase64Correct);
            Media expectedMedia = new Media()
            {
                Content = image,
                ContentSize = image.Length,
                Id = "E9FCFA44-97D0-4D97-8339-62FD41930A3C",
                FileName = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"
            };
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            _dataService.Setup(x => x.GetPicture(It.IsAny<string>())).Returns(Task.FromResult<string>(expectedMedia.FileName));
            _mediaStorageAppService.Setup(x => x.SaveMedia(It.IsAny<byte[]>())).Returns(expectedMedia.FileName);
            _mediaStorageAppService.Setup(x => x.GetMedia(It.IsAny<string>())).Returns(expectedMedia.Content);
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);
            // act
            var actualMedia = await apiService.UpdatePicture(stringBase64Correct);

            // assert
            Assert.NotNull(actualMedia);
            Assert.AreEqual(expectedMedia.Content, actualMedia.Content);
            Assert.AreEqual(expectedMedia.ContentSize, actualMedia.ContentSize);
            Assert.AreEqual(expectedMedia.Id, actualMedia.Id);
            Assert.AreEqual(expectedMedia.FileName, actualMedia.FileName);
        }

        [Test]
        public void Should_Not_Update_User_Picture_When_File_Size_Is_Too_Big()
        {
            // arrange
            var stringBase64Wrong = PictureHelper.GetPictureBase64WithHugeSize();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.UpdatePicture(stringBase64Wrong, "F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Update_Authenticated_User_Picture_When_File_Size_Is_Too_Big()
        {
            // arrange
            var stringBase64Wrong = PictureHelper.GetPictureBase64WithHugeSize();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.UpdatePicture(stringBase64Wrong));
        }

        [Test]
        public void Should_Not_Update_User_Picture_When_Image_File_Is_Wrong()
        {
            // arrange
            var stringBase64 = PictureHelper.GetWrongPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.UpdatePicture(stringBase64, "F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Update_User_Authenticated_Picture_When_Image_File_Is_Wrong()
        {
            // arrange
            var stringBase64 = PictureHelper.GetWrongPictureBase64();
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.UpdatePicture(stringBase64));
        }

        [Test]
        public void Should_Not_Update_Authenticated_User_Picture_When_Data_Service_Throws_Exception()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            var image = Convert.FromBase64String(stringBase64Correct);
            Media expectedMedia = new Media()
            {
                Content = image,
                ContentSize = image.Length,
                Id = "E9FCFA44-97D0-4D97-8339-62FD41930A3C",
                FileName = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"
            };
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test", UserId = "A9FCFA44-97D0-4D97-8339-62FD41930A3C" });

            _dataService.Setup(x => x.UpdatePicture(It.IsAny<string>(), It.IsAny<string>())).Throws<BusinessException>();
            _dataService.Setup(x => x.GetPicture(It.IsAny<string>())).Returns(Task.FromResult<string>(expectedMedia.FileName));
            _mediaStorageAppService.Setup(x => x.SaveMedia(It.IsAny<byte[]>())).Returns(expectedMedia.FileName);
            _mediaStorageAppService.Setup(x => x.GetMedia(It.IsAny<string>())).Returns(expectedMedia.Content);
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.UpdatePicture(stringBase64Correct));
        }

        [Test]
        public void Should_Not_Return_User_Picture_When_Data_Service_Throws_Exception()
        {
            // arrange
            _dataService.Setup(x => x.GetPicture(It.IsAny<string>())).Throws<BusinessException>();
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.GetPicture("F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Return_Authenticated_User_Picture_When_Data_Service_Throws_Exception()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.GetPicture(It.IsAny<string>())).Throws<BusinessException>();
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () =>
                await apiService.GetPicture());
        }

       
        [Test]
        public void Should_Not_Return_User_Picture_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.GetPicture(It.IsAny<string>()))
                .Throws<BusinessException>();

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.GetPicture("F9FCFA44-97D0-4D97-8339-62FD41930A3C"));
        }

        [Test]
        public void Should_Not_Return_Authenticated_User_Picture_And_Throw_Exception_When_Session_Context_Is_Null()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns((ISessionContext)null);

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.GetPicture());
        }

        [Test]
        public void Should_Not_Return_Authenticated_User_Picture_And_Throw_Exception_When_Data_Service_Throws_Exception()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            _dataService.Setup(x => x.GetPicture(It.IsAny<string>()))
                .Throws<BusinessException>();

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.GetPicture());
        }

        [Test]
        public void Should_Not_Return_Authenticated_User_Picture_And_Throw_Exception_When_User_Is_Not_Authenticated()
        {
            // arrange
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "", UserId = "" });

            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.GetPicture());
        }

        [Test]
        public async Task Should_Return_User_Picture()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            var image = Convert.FromBase64String(stringBase64Correct);
            Media expectedMedia = new Media()
            {
                Content = image,
                ContentSize = image.Length,
                Id = "E9FCFA44-97D0-4D97-8339-62FD41930A3C",
                FileName = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"
            };
            _dataService.Setup(x => x.GetPicture(It.IsAny<string>())).Returns(Task.FromResult<string>(expectedMedia.FileName));
            _mediaStorageAppService.Setup(x => x.GetMedia(It.IsAny<string>())).Returns(expectedMedia.Content);
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);
            // act
            var actualMedia = await apiService.GetPicture("F9FCFA44-97D0-4D97-8339-62FD41930A3C");

            // assert
            Assert.NotNull(actualMedia);
            Assert.AreEqual(expectedMedia.Content, actualMedia.Content);
            Assert.AreEqual(expectedMedia.ContentSize, actualMedia.ContentSize);
            Assert.AreEqual(expectedMedia.Id, actualMedia.Id);
            Assert.AreEqual(expectedMedia.FileName, actualMedia.FileName);
        }

        [Test]
        public async Task Should_Return_Authenticated_User_Picture()
        {
            // arrange
            var stringBase64Correct = PictureHelper.GetCorrectPictureBase64();
            var image = Convert.FromBase64String(stringBase64Correct);
            Media expectedMedia = new Media()
            {
                Content = image,
                ContentSize = image.Length,
                Id = "E9FCFA44-97D0-4D97-8339-62FD41930A3C",
                FileName = "E9FCFA44-97D0-4D97-8339-62FD41930A3C"
            };
            _dataService.Setup(x => x.GetPicture(It.IsAny<string>())).Returns(Task.FromResult<string>(expectedMedia.FileName));
            _mediaStorageAppService.Setup(x => x.GetMedia(It.IsAny<string>())).Returns(expectedMedia.Content);
            _provider.Setup(x => x.GetContext())
                .Returns(new SessionContext() { UserEmail = "test@test.pl", UserId = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E" });
            var apiService = new UsersApiService(_provider.Object, _dataService.Object, _mediaStorageAppService.Object, _mapper, _logger.Object);
            // act
            var actualMedia = await apiService.GetPicture();

            // assert
            Assert.NotNull(actualMedia);
            Assert.AreEqual(expectedMedia.Content, actualMedia.Content);
            Assert.AreEqual(expectedMedia.ContentSize, actualMedia.ContentSize);
            Assert.AreEqual(expectedMedia.Id, actualMedia.Id);
            Assert.AreEqual(expectedMedia.FileName, actualMedia.FileName);
        }

    }
}
