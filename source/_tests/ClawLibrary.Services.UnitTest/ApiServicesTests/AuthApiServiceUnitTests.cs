using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.Models.Auth;
using ClawLibrary.Core.Models.Users;
using ClawLibrary.Mail;
using ClawLibrary.Services.ApiServices;
using ClawLibrary.Services.Mapping;
using ClawLibrary.Services.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ClawLibrary.Services.UnitTests.ApiServicesTests
{
    [TestFixture]
    public class AuthApiServiceUnitTests
    {
        private AuthConfig _config;
        private Mock<IPasswordHasher> _passwordHasher;
        private Mock<IAuthDataService> _dataService;
        private IMapper _mapper;
        private Mock<TokenProviderOptions> _options;
        private Mock<ILogger<AuthApiService>> _logger;

        private Mock<IMailGenerator> _mailGenerator;
        private Mock<IMailSender> _mailSender;

        [SetUp]
        protected void SetUp()
        {
            _config = new AuthConfig();
            _passwordHasher = new Mock<IPasswordHasher>();
            _dataService = new Mock<IAuthDataService>();
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }));
            _options = new Mock<TokenProviderOptions>();
            _logger = new Mock<ILogger<AuthApiService>>();
            _mailGenerator = new Mock<IMailGenerator>();
            _mailSender = new Mock<IMailSender>();
        }

        [Test]
        public async Task Should_Authorize_User()
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
            var expectedExpiresAt = DateTimeOffset.Now.AddDays(1);
            var expectedUserRoles = new List<string>(){ "Admin", "Regular" };
            _passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _dataService.Setup(x => x.GetUserRoles(It.IsAny<long>())).Returns(Task.FromResult<List<string>>(expectedUserRoles));
            
            var apiService = new AuthApiService(_config, _dataService.Object,  _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                                                     _mapper, _options.Object, _logger.Object);
            var request = new AuthorizeRequest()
            {
                Email = expectedUser.Email,
                Password = "test"
            };
            var handler = new JwtSecurityTokenHandler();

            // act
            var token = await apiService.Authorize(request);
            var jsonToken = handler.ReadJwtToken(token.AccessToken);
            var roles = jsonToken.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(x=>x.Value);
            var userEmail = jsonToken.Claims.First(claim => claim.Type == ClaimTypes.GroupSid).Value;
            var userKey = jsonToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

            // assert
            Assert.NotNull(token);
            Assert.AreEqual(expectedUserRoles, roles);
            Assert.AreEqual(expectedUser.Email.ToLower(), userEmail.ToLower());
            Assert.AreEqual(expectedUser.Key, userKey);
            Assert.AreEqual(token.ExpiresAt.Date, expectedExpiresAt.Date);
        }
        
        [TestCase(Status.Deleted)]
        [TestCase(Status.Active)]
        [TestCase(Status.Pending)]
        public void Should_Throw_Exception_When_User_Status_Is_Not_Active(Status status)
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
                Status = status.ToString()

            };
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);
            var request = new AuthorizeRequest()
            {
                Email = "test",
                Password = "test"
            };

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.Authorize(request));
        }

        [Test]
        public void Should_Throw_Exception_When_User_Does_Not_Exist()
        {
            // arrange
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new AuthorizeRequest()
            {
                Email = "test",
                Password = "test"
            };

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.Authorize(request));
        }

        [Test]
        public void Should_Throw_Exception_When_User_Does_Not_Have_Any_Role()
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

            var expectedUserRoles = new List<string>();

            _passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _dataService.Setup(x => x.GetUserRoles(It.IsAny<long>())).Returns(Task.FromResult<List<string>>(expectedUserRoles));

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new AuthorizeRequest()
            {
                Email = "test",
                Password = "test"
            };

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.Authorize(request));
        }

        [Test]
        public void Should_Register_User()
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
            _dataService.Setup(x => x.RegisterUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult<User>(expectedUser));
            _passwordHasher.Setup(x => x.HashPassword(It.IsAny<string>()))
                .Returns("AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==");

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new RegisterUserRequest()
            {
                Email = expectedUser.Email,
                Password = expectedUser.Password,
                ConfirmPassword = expectedUser.Password,
                FirstName = expectedUser.FirstName,
                LastName = expectedUser.LastName
            };

            // act & assert
            Assert.DoesNotThrowAsync(async () => await apiService.RegisterUser(request));
        }

        [Test]
        public void Should_Not_Register_User_When_Data_Service_Throws_Exception()
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
            _dataService.Setup(x => x.RegisterUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws<BusinessException>();
            _passwordHasher.Setup(x => x.HashPassword(It.IsAny<string>()))
                .Returns("AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==");

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new RegisterUserRequest()
            {
                Email = expectedUser.Email,
                Password = expectedUser.Password,
                ConfirmPassword = expectedUser.Password,
                FirstName = expectedUser.FirstName,
                LastName = expectedUser.LastName
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.RegisterUser(request));
        }

        [Test]
        public async Task Should_Verify_User()
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
                Status = Status.Pending.ToString()
            };
            var expectedUser2 = new Core.Models.Users.User()
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
            var handler = new JwtSecurityTokenHandler();
            var expectedExpiresAt = DateTimeOffset.Now.AddDays(1);
            var expectedUserRoles = new List<string>() { "Admin", "Regular" };
            _passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);
            _dataService.SetupSequence(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser))
                                                                                 .Returns(Task.FromResult<User>(expectedUser2));

            _dataService.Setup(x => x.GetUserRoles(It.IsAny<long>())).Returns(Task.FromResult<List<string>>(expectedUserRoles));
            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new UserVerificationRequest()
            {
                Email = expectedUser.Email,
                Password = expectedUser.Password,
                VerificationCode = expectedUser.Key
            };            
            
            // act
            var token = await apiService.VerifyUser(request);
            var jsonToken = handler.ReadJwtToken(token.AccessToken);
            var roles = jsonToken.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(x => x.Value);
            var userEmail = jsonToken.Claims.First(claim => claim.Type == ClaimTypes.GroupSid).Value;
            var userKey = jsonToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

            // assert
            Assert.NotNull(token);
            Assert.AreEqual(expectedUserRoles, roles);
            Assert.AreEqual(expectedUser.Email.ToLower(), userEmail.ToLower());
            Assert.AreEqual(expectedUser.Key, userKey);
            Assert.AreEqual(token.ExpiresAt.Date, expectedExpiresAt.Date);
        }

        [Test]
        public void Should_Not_Verify_User_And_Throw_Exception_When_User_Does_Not_Exist()
        {
            // arrange
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

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

            var request = new UserVerificationRequest()
            {
                Email = expectedUser.Email,
                Password = expectedUser.Password,
                VerificationCode = expectedUser.Key
            };

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.VerifyUser(request));
        }

        [Test]
        public void Should_Not_Verify_User_And_Throw_Exception_When_User_Does_Not_Have_Any_Role()
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
                Status = Status.Pending.ToString()

            };

            var expectedUserRoles = new List<string>();

            _passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _dataService.Setup(x => x.GetUserRoles(It.IsAny<long>())).Returns(Task.FromResult<List<string>>(expectedUserRoles));

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new UserVerificationRequest()
            {
                Email = expectedUser.Email,
                Password = expectedUser.Password,
                VerificationCode = expectedUser.Key
            };

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.VerifyUser(request));
        }

        [Test]
        public void Should_Not_Verify_User_And_Throw_Exception_When_User_Does_Not_Have_Role_Admin()
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
                Status = Status.Pending.ToString()

            };

            var expectedUserRoles = new List<string>() { "Regular" };

            _passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _dataService.Setup(x => x.GetUserRoles(It.IsAny<long>())).Returns(Task.FromResult<List<string>>(expectedUserRoles));

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new UserVerificationRequest()
            {
                Email = expectedUser.Email,
                Password = expectedUser.Password,
                VerificationCode = expectedUser.Key
            };

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.VerifyUser(request));
        }

        [Test]
        public void Should_Not_Verify_User_And_Throw_Exception_When_Password_Verification_Failed()
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
                Status = Status.Pending.ToString()

            };

            var expectedUserRoles = new List<string>() { "Admin" };

            _passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Failed);
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _dataService.Setup(x => x.GetUserRoles(It.IsAny<long>())).Returns(Task.FromResult<List<string>>(expectedUserRoles));

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new UserVerificationRequest()
            {
                Email = expectedUser.Email,
                Password = expectedUser.Password,
                VerificationCode = expectedUser.Key
            };

            // act & assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await apiService.VerifyUser(request));
        }

        [Test]
        public void Should_Not_Verify_User_And_Throw_Exception_When_Verification_Code_Is_Wrong()
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
                Status = Status.Pending.ToString()

            };

            var expectedUserRoles = new List<string>() { "Admin" };

            _passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _dataService.Setup(x => x.GetUserRoles(It.IsAny<long>())).Returns(Task.FromResult<List<string>>(expectedUserRoles));

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new UserVerificationRequest()
            {
                Email = expectedUser.Email,
                Password = expectedUser.Password,
                VerificationCode = "test"
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.VerifyUser(request));
        }

        [TestCase(Status.Deleted)]
        [TestCase(Status.Active)]
        [TestCase(Status.Inactive)]
        public void Should_Not_Verify_User_And_Throw_Exception_When_User_Status_Is_Wrong(Status status)
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
                Status = status.ToString()

            };

            var expectedUserRoles = new List<string>() { "Admin" };

            _passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _dataService.Setup(x => x.GetUserRoles(It.IsAny<long>())).Returns(Task.FromResult<List<string>>(expectedUserRoles));

            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new UserVerificationRequest()
            {
                Email = expectedUser.Email,
                Password = expectedUser.Password,
                VerificationCode = "test"
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.VerifyUser(request));
        }

        [Test]
        public void Should_Reset_Password()
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
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new ResetUserPasswordRequest()
            {
                Email = expectedUser.Email
            };

            // act & assert
            Assert.DoesNotThrowAsync(async () => await apiService.ResetPassword(request));
        }

        [Test]
        public void Should_Not_Reset_Password_And_Throw_Exception_When_User_Does_Not_Exist()
        {
            // arrange
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(null));
            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new ResetUserPasswordRequest()
            {
                Email = "test@test.pl"
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.ResetPassword(request));
        }

        [Test]
        public void Should_Not_Reset_Password_And_Throw_Exception_When_Data_Service_Throws_Exception()
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
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _dataService.Setup(x => x.CreatePasswordResetKey(It.IsAny<long>(), It.IsAny<string>()))
                .Throws<BusinessException>();
            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);
            var request = new ResetUserPasswordRequest()
            {
                Email = expectedUser.Email
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.ResetPassword(request));
        }

        [Test]
        public void Should_Set_Password()
        {
            // arrange
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "David.Schwimmer@test.com",
                FirstName = "David",
                LastName = "Schwimmer",
                
                PasswordResetKey = "9855C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = DateTimeOffset.Now,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Active.ToString()
            };
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _passwordHasher.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(expectedUser.Password);
            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new SetUserPasswordRequest()
            {
                Email = expectedUser.Email,
                Password = "test",
                ConfirmPassword ="test",
                VerificationCode = expectedUser.PasswordResetKey
            };

            // act & assert
            Assert.DoesNotThrowAsync(async () => await apiService.SetPassword(request));
        }

        [Test]
        public void Should_Not_Set_Password_And_Throw_Exception_When_User_Does_Not_Exist()
        {
            // arrange
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(null));
            _passwordHasher.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(Guid.NewGuid().ToString());
            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new SetUserPasswordRequest()
            {
                Email = "test@test.pl",
                Password = "test",
                ConfirmPassword = "test",
                VerificationCode = Guid.NewGuid().ToString()
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.SetPassword(request));
        }

        [Test]
        public void Should_Not_Set_Password_And_Throw_Exception_When_Password_Reset_Key_Expired()
        {
            // arrange
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "David.Schwimmer@test.com",
                FirstName = "David",
                LastName = "Schwimmer",
                
                PasswordResetKey = "9855C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = DateTimeOffset.Now.AddDays(-2),
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Active.ToString()
            };
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _passwordHasher.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(Guid.NewGuid().ToString());
            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new SetUserPasswordRequest()
            {
                Email = "test@test.pl",
                Password = "test",
                ConfirmPassword = "test",
                VerificationCode = "9855C5CA-8BAA-4C69-8E9A-AA17AE51138E"
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.SetPassword(request));
        }

        [Test]
        public void Should_Not_Set_Password_And_Throw_Exception_When_Verification_Code_Is_Wrong()
        {
            // arrange
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "David.Schwimmer@test.com",
                FirstName = "David",
                LastName = "Schwimmer",
                
                PasswordResetKey = "9855C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = DateTimeOffset.Now,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Active.ToString()
            };
            _dataService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            _passwordHasher.Setup(x => x.HashPassword(It.IsAny<string>())).Returns(Guid.NewGuid().ToString());
            var apiService = new AuthApiService(_config, _dataService.Object, _passwordHasher.Object, _mailGenerator.Object, _mailSender.Object,
                _mapper, _options.Object, _logger.Object);

            var request = new SetUserPasswordRequest()
            {
                Email = "test@test.pl",
                Password = "test",
                ConfirmPassword = "test",
                VerificationCode = Guid.NewGuid().ToString()
            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await apiService.SetPassword(request));
        }
    }
}
