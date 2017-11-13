using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Data.DataServices;
using ClawLibrary.Data.Mapping;
using ClawLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ClawLibrary.Data.UnitTests.DataServicesTests
{
    [TestFixture]
    public class AuthDataServiceUnitTests
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        private List<User> _data;

        public AuthDataServiceUnitTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DataMappingProfile());
            }));
        }

        [SetUp]
        protected void SetUp()
        {
            _data = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Key = new Guid("9955C5CA-8BAA-4C69-8E9A-AA17AE51138E"),
                    Email = "test@test.com",
                    PasswordSalt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                    PasswordHash = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                    FirstName = "Jon",
                    LastName = "Snow",
                    PhoneNumber = "123456789",
                    PasswordResetKey = null,
                    PasswordResetKeyCreatedDate = null,
                    ImageFileId = 1,
                    CreatedDate = DateTimeOffset.Now,
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = Status.Active.ToString(),
                    ImageFile =
                        new File()
                        {
                            FileName = "testFileName",
                            Key = Guid.NewGuid(),
                            Id = 1,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString()
                        },
                    UserRole = new List<UserRole>()
                    {
                        new UserRole()
                        {
                            Id = 1,
                            Key = new Guid("B89C802A-9992-41C3-8ED1-23F99E630D46"),
                            UserId = 1,
                            RoleId = 1,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 1,
                                Key = new Guid("335E6351-CA14-4885-A129-8FFA756F5897"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Admin"
                            }
                        },
                        new UserRole()
                        {
                            Id = 2,
                            Key = new Guid("4ED72EC8-4CC4-430A-8163-D7F1BE8F1521"),
                            UserId = 1,
                            RoleId = 2,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 2,
                                Key = new Guid("64475D39-8EBC-40D9-9795-DC5D95DE6208"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Regular"
                            }
                        }
                    }
                },
                new User()
                {
                    Id = 2,
                    Key = new Guid("E9FCFA44-97D0-4D97-8339-62FD41930A3C"),
                    Email = "test2@test2.com",
                    PasswordSalt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                    PasswordHash = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                    FirstName = "Tom",
                    LastName = "Cruise",
                    PhoneNumber = "123456789",
                    PasswordResetKey = "6968DB0F-6059-4B23-865A-317D70F46268",
                    PasswordResetKeyCreatedDate = DateTimeOffset.Now,
                    ImageFileId = null,
                    CreatedDate = DateTimeOffset.Now,
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = Status.Active.ToString(),
                    ImageFile = null,
                    UserRole = new List<UserRole>()
                    {
                        new UserRole()
                        {
                            Id = 3,
                            Key = new Guid("B89C802A-9992-41C3-8ED1-23F99E630D46"),
                            UserId = 2,
                            RoleId = 1,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Inactive.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 1,
                                Key = new Guid("335E6351-CA14-4885-A129-8FFA756F5897"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Admin"
                            }
                        },
                        new UserRole()
                        {
                            Id = 4,
                            Key = new Guid("4ED72EC8-4CC4-430A-8163-D7F1BE8F1521"),
                            UserId = 2,
                            RoleId = 2,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 2,
                                Key = new Guid("64475D39-8EBC-40D9-9795-DC5D95DE6208"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Regular"
                            }
                        }
                    }
                },
                new User()
                {
                    Id = 3,
                    Key = new Guid("2DBF6780-9500-427E-A9C5-F843611D0BBC"),
                    Email = "test3@test3.com",
                    PasswordSalt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                    PasswordHash = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                    FirstName = "Chris",
                    LastName = "Hemsworth",
                    PhoneNumber = "123456789",
                    PasswordResetKey = null,
                    PasswordResetKeyCreatedDate = null,
                    ImageFileId = null,
                    CreatedDate = DateTimeOffset.Now,
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = Status.Active.ToString(),
                    ImageFile = null,
                    UserRole = new List<UserRole>()
                    {
                        new UserRole()
                        {
                            Id = 5,
                            Key = new Guid("641C1559-3877-491C-8E58-50A86A7CFE66"),
                            UserId = 3,
                            RoleId = 1,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 1,
                                Key = new Guid("335E6351-CA14-4885-A129-8FFA756F5897"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Admin"
                            }
                        },
                        new UserRole()
                        {
                            Id = 6,
                            Key = new Guid("6674061E-5A57-416D-96CE-4F7B6A5CB46E"),
                            UserId = 3,
                            RoleId = 2,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Inactive.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 2,
                                Key = new Guid("64475D39-8EBC-40D9-9795-DC5D95DE6208"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Regular"
                            }
                        }
                    }
                },
                new User()
                {
                    Id = 4,
                    Key = new Guid("B1AC0434-9A0A-42EB-A769-8A2569027522"),
                    Email = "test4@test4.com",
                    PasswordSalt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                    PasswordHash = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                    FirstName = "Bradley",
                    LastName = "Cooper",
                    PhoneNumber = "123456789",
                    PasswordResetKey = null,
                    PasswordResetKeyCreatedDate = null,
                    ImageFileId = null,
                    CreatedDate = DateTimeOffset.Now,
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = Status.Active.ToString(),
                    ImageFile = null,
                    UserRole = new List<UserRole>()
                    {
                        new UserRole()
                        {
                            Id = 7,
                            Key = new Guid("D8DCB934-7790-41BD-AC11-23B1BFD3C264"),
                            UserId = 4,
                            RoleId = 1,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Inactive.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 1,
                                Key = new Guid("335E6351-CA14-4885-A129-8FFA756F5897"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Admin"
                            }
                        },
                        new UserRole()
                        {
                            Id = 8,
                            Key = new Guid("46CBBF11-264C-46E9-A338-1DE8E3A54160"),
                            UserId = 4,
                            RoleId = 2,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Inactive.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 2,
                                Key = new Guid("64475D39-8EBC-40D9-9795-DC5D95DE6208"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Regular"
                            }
                        }
                    }
                },
                new User()
                {
                    Id = 5,
                    Key = new Guid("9CE78CDA-6366-43CC-B746-E9D0279C89DB"),
                    Email = "test5@test5.com",
                    PasswordSalt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                    PasswordHash = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                    FirstName = "Omar",
                    LastName = "Sy",
                    PhoneNumber = "123456789",
                    PasswordResetKey = null,
                    PasswordResetKeyCreatedDate = null,
                    ImageFileId = null,
                    CreatedDate = DateTimeOffset.Now,
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = Status.Pending.ToString(),
                    ImageFile = null,
                    UserRole = new List<UserRole>()
                    {
                        new UserRole()
                        {
                            Id = 9,
                            Key = new Guid("C06B8307-4F67-434D-A67E-46E0A74ECE0A"),
                            UserId = 5,
                            RoleId = 1,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 1,
                                Key = new Guid("335E6351-CA14-4885-A129-8FFA756F5897"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Admin"
                            }
                        },
                        new UserRole()
                        {
                            Id = 10,
                            Key = new Guid("04ABB80C-6713-4331-906D-8F1D0D3CB67E"),
                            UserId = 5,
                            RoleId = 2,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 2,
                                Key = new Guid("64475D39-8EBC-40D9-9795-DC5D95DE6208"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Regular"
                            }
                        }
                    }
                },
                new User()
                {
                    Id = 6,
                    Key = new Guid("9CE78CDA-6366-43CC-B746-E9D0279C89DB"),
                    Email = "test6@test6.com",
                    PasswordSalt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                    PasswordHash = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                    FirstName = "Sam",
                    LastName = "Keeley",
                    PhoneNumber = "123456789",
                    PasswordResetKey = null,
                    PasswordResetKeyCreatedDate = null,
                    ImageFileId = null,
                    CreatedDate = DateTimeOffset.Now,
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = Status.Inactive.ToString(),
                    ImageFile = null,
                    UserRole = new List<UserRole>()
                    {
                        new UserRole()
                        {
                            Id = 11,
                            Key = new Guid("6A0734F0-B3DE-44D6-93E3-F9E504A35A6E"),
                            UserId = 6,
                            RoleId = 1,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 1,
                                Key = new Guid("335E6351-CA14-4885-A129-8FFA756F5897"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Admin"
                            }
                        },
                        new UserRole()
                        {
                            Id = 12,
                            Key = new Guid("7FB56B07-18E8-4F01-9FB8-5A474A59ADCC"),
                            UserId = 6,
                            RoleId = 2,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 2,
                                Key = new Guid("64475D39-8EBC-40D9-9795-DC5D95DE6208"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Regular"
                            }
                        }
                    }
                },
                new User()
                {
                    Id = 7,
                    Key = new Guid("8BEB61E6-50CA-417E-9784-690E32B905F6"),
                    Email = "test7@test7.com",
                    PasswordSalt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                    PasswordHash = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                    FirstName = "Riccardo",
                    LastName = "Scamarcio",
                    PhoneNumber = "123456789",
                    PasswordResetKey = null,
                    PasswordResetKeyCreatedDate = null,
                    ImageFileId = null,
                    CreatedDate = DateTimeOffset.Now,
                    CreatedBy = "System",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    Status = Status.Deleted.ToString(),
                    ImageFile = null,
                    UserRole = new List<UserRole>()
                    {
                        new UserRole()
                        {
                            Id = 13,
                            Key = new Guid("433470CD-6E7D-4D2D-9FE7-C0FA483E93FF"),
                            UserId = 7,
                            RoleId = 1,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 1,
                                Key = new Guid("335E6351-CA14-4885-A129-8FFA756F5897"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Admin"
                            }
                        },
                        new UserRole()
                        {
                            Id = 14,
                            Key = new Guid("6968DB0F-6059-4B23-865A-317D70F46268"),
                            UserId = 7,
                            RoleId = 2,
                            CreatedBy = "System",
                            CreatedDate = DateTimeOffset.Now,
                            Status = Status.Active.ToString(),
                            Role = new ClawLibrary.Data.Models.Role()
                            {
                                Id = 2,
                                Key = new Guid("64475D39-8EBC-40D9-9795-DC5D95DE6208"),
                                Status = Status.Active.ToString(),
                                CreatedBy = "System",
                                CreatedDate = DateTimeOffset.Now,
                                Name = "Regular"
                            }
                        }
                    }
                }
            };

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DatabaseContext(options);

            foreach (var user in _data)
            {
                context.User.Add(user);
            }
            context.SaveChanges();
            _context = context;
        }

        [TestCase("test@test.com", 1)]
        [TestCase("test2@test2.com", 2)]
        [TestCase("test3@test3.com", 3)]
        [TestCase("test4@test4.com", 4)]
        [TestCase("test5@test5.com", 5)]
        [TestCase("test6@test6.com", 6)]
        public async Task Should_Return_User_With_Specified_Email(string email, int expectedId)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper,_context);

            // act
            var user = await authDataService.GetUserByEmail(email);

            // assert
            Assert.NotNull(user);
            Assert.AreEqual(expectedId, user.Id);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase(null)]
        public void Should_Not_Return_User_When_Email_Is_Wrong(string email)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await authDataService.GetUserByEmail(email));
        }

        [TestCase("test7@test7.com")]
        [TestCase("test8@test8.com")]
        [TestCase("test3")]
        public async Task Should_Not_Return_User_When_User_With_Email_Does_Not_Exist(string email)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);

            // act
            var user = await authDataService.GetUserByEmail(email);

            // assert
            Assert.Null(user);
        }

        [TestCase(1, "Admin", "Regular")]
        [TestCase(2, null, "Regular")]
        [TestCase(3, "Admin", null)]
        [TestCase(4, null, null)]
        [TestCase(5, "Admin", "Regular")]
        [TestCase(6, "Admin", "Regular")]
        public async Task Should_Return_User_Roles(long userId, string expectedRole1, string expectedRole2)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);
            var expectedUserRoles = new List<string>();

            if (!string.IsNullOrEmpty(expectedRole1))
                expectedUserRoles.Add(expectedRole1);
            if (!string.IsNullOrEmpty(expectedRole2))
                expectedUserRoles.Add(expectedRole2);

            // act
            var userRoles = await authDataService.GetUserRoles(userId);

            // assert
            Assert.AreEqual(expectedUserRoles.Count, userRoles.Count);
            foreach (var userRole in userRoles)
            {
                Assert.True(expectedUserRoles.Any(x=>x.Equals(userRole)));
            }
        }

        [Test]
        public async Task Should_Create_New_User()
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Email = "David.Schwimmer@test.com",
                FirstName = "David",
                LastName = "Schwimmer",
                PhoneNumber = "789456123",
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Pending.ToString()
                
            };
            var expectedRolesCount = _context.Role.Count();

            // act
            var actualUser = await authDataService.RegisterUser(expectedUser, expectedUser.Password,
                expectedUser.Salt);
            var userRoles = _context.UserRole.Include("Role").Where(x => x.UserId == actualUser.Id);
            
            // assert
            Assert.NotNull(actualUser);
            Assert.NotNull(userRoles);
            Assert.AreEqual(expectedRolesCount, userRoles.Count());
            Assert.True(userRoles.Any(x=>x.Role.Name.ToLower().Equals(Core.Enums.Role.Regular.ToString().ToLower()) && x.Status.ToLower().Equals(Status.Active.ToString().ToLower())));
            Assert.True(userRoles.Any(x => x.Role.Name.ToLower().Equals(Core.Enums.Role.Admin.ToString().ToLower()) && x.Status.ToLower().Equals(Status.Inactive.ToString().ToLower())));
            Assert.AreEqual(expectedUser.Id, actualUser.Id);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.PhoneNumber, actualUser.PhoneNumber);
            Assert.AreEqual(expectedUser.PasswordResetKey, actualUser.PasswordResetKeyCreatedDate);
            Assert.AreEqual(expectedUser.CreatedDate.Date, actualUser.CreatedDate.Date);
            Assert.AreEqual(expectedUser.ModifiedDate, actualUser.ModifiedDate);
            Assert.AreEqual(expectedUser.Status.ToString(), actualUser.Status.ToString());
            Assert.AreEqual(expectedUser.Salt, actualUser.Salt);
        }

        [Test]
        public async Task Should_Create_New_User_When_User_With_Email_Exists_But_Has_Been_Deleted()
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);

            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Email = "test7@test7.com",
                FirstName = "Riccardo",
                LastName = "Scamarcio",
                PhoneNumber = "1234567879",
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Pending.ToString(),

            };
            var expectedRolesCount = _context.Role.Count();

            // act
            var actualUser = await authDataService.RegisterUser(expectedUser, expectedUser.Password,
                expectedUser.Salt);
            var userRoles = _context.UserRole.Include("Role").Where(x => x.UserId == actualUser.Id);

            // assert
            Assert.NotNull(actualUser);
            Assert.NotNull(userRoles);
            Assert.AreEqual(expectedRolesCount, userRoles.Count());
            Assert.True(userRoles.Any(x => x.Role.Name.ToLower().Equals(Core.Enums.Role.Regular.ToString().ToLower()) && x.Status.ToLower().Equals(Status.Active.ToString().ToLower())));
            Assert.True(userRoles.Any(x => x.Role.Name.ToLower().Equals(Core.Enums.Role.Admin.ToString().ToLower()) && x.Status.ToLower().Equals(Status.Inactive.ToString().ToLower())));
            Assert.AreEqual(expectedUser.Id, actualUser.Id);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.PhoneNumber, actualUser.PhoneNumber);
            Assert.AreEqual(expectedUser.PasswordResetKey, actualUser.PasswordResetKeyCreatedDate);
            Assert.AreEqual(expectedUser.CreatedDate.Date, actualUser.CreatedDate.Date);
            Assert.AreEqual(expectedUser.ModifiedDate, actualUser.ModifiedDate);
            Assert.AreEqual(expectedUser.Status.ToString(), actualUser.Status.ToString());
            Assert.AreEqual(expectedUser.Salt, actualUser.Salt);
        }

        [TestCase("test@test.com")]
        [TestCase("test2@test2.com")]
        [TestCase("test3@test3.com")]
        [TestCase("test4@test4.com")]
        [TestCase("test5@test5.com")]
        [TestCase("test6@test6.com")]
        public void Should_Not_Create_New_User_When_User_With_Email_Exists(string email)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Email = email,
                FirstName = "David",
                LastName = "Schwimmer",
                PhoneNumber = "789456123",
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Pending.ToString(),

            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await authDataService.RegisterUser(expectedUser, expectedUser.Password,
                expectedUser.Salt));
        }

        [TestCase("123456789")]
        public void Should_Not_Create_New_User_When_User_With_Phone_Exists(string phoneNumber)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 8,
                Email = "2test@test2.com",
                FirstName = "David",
                LastName = "Schwimmer",
                PhoneNumber = phoneNumber,
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Pending.ToString(),

            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await authDataService.RegisterUser(expectedUser, expectedUser.Password,
                expectedUser.Salt));
        }
        
        [Test]
        public async Task Should_Change_User_Status_To_Active()
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);
            var expectedUser = _data.FirstOrDefault(x => x.Status.ToLower()
                .Equals(Status.Pending.ToString().ToLower()));


            // act
            if (expectedUser != null)
            {
                await authDataService.VerifyUser(expectedUser.Id);
                var actualUser = _context.User.FirstOrDefault(x => x.Id == expectedUser.Id);

                // assert
                Assert.NotNull(actualUser);
                Assert.AreEqual(Status.Active.ToString().ToLower(), actualUser.Status.ToLower());
                Assert.NotNull(actualUser.ModifiedDate);
                Assert.NotNull(actualUser.ModifiedBy);
                Assert.AreEqual(DateTimeOffset.Now.Date, actualUser.ModifiedDate?.Date);
                Assert.AreEqual(expectedUser.Email.ToLower(), actualUser.ModifiedBy.ToLower());
            }
            else
                throw new Exception("Missing correct mock data!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(111122112)]
        [TestCase(7)]
        public void Should_Not_Change_User_Status_And_Should_Throw_Exception_When_User_Does_Not_Exist(long userId)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await authDataService.VerifyUser(userId));
        }

        [TestCase(1)]
        [TestCase(6)]
        public void Should_Not_Change_User_Status_And_Should_Throw_Exception_When_User_Status_Is_Different_Than_Pending(long userId)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await authDataService.VerifyUser(userId));
        }

        [TestCase(1)]
        [TestCase(6)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public async Task Should_Create_Password_Reset_Key(long userId)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);
            var expectedPasswordResetKey = "7FB56B07-18E8-4F01-9FB8-5A474A59ADCC";

            // act
            await authDataService.CreatePasswordResetKey(userId, expectedPasswordResetKey);
            var actualUser = _context.User.FirstOrDefault(x => x.Id == userId);

            // assert
            Assert.NotNull(actualUser);
            Assert.AreEqual(expectedPasswordResetKey.ToLower(), actualUser.PasswordResetKey.ToLower());
            Assert.NotNull(actualUser.PasswordResetKeyCreatedDate);
            Assert.AreEqual(DateTimeOffset.Now.Date, actualUser.PasswordResetKeyCreatedDate.Value.Date);
            Assert.NotNull(actualUser.ModifiedDate);
            Assert.AreEqual(DateTimeOffset.Now.Date, actualUser.ModifiedDate.Value.Date);
            Assert.NotNull(actualUser.ModifiedBy);
            Assert.AreEqual(actualUser.Email, actualUser.ModifiedBy);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(111122112)]
        [TestCase(7)]
        public void Should_Not_Create_Password_Reset_Key_And_Should_Throw_Exception_When_User_Does_Not_Exist(long userId)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await authDataService.CreatePasswordResetKey(userId, "7FB56B07-18E8-4F01-9FB8-5A474A59ADCC"));
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase("     ")]
        [TestCase(" ")]
        public void Should_Not_Create_Password_Reset_Key_And_Should_Throw_Exception_When_Password_Reset_Key_Is_Null_Or_Empty(string passwordResetKey)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await authDataService.CreatePasswordResetKey(1, passwordResetKey));
        }

        [Test]
        public async Task Should_Reset_Password()
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);
            var expectedUser = new User()
            {
                Id = 2,
                Key = new Guid("E9FCFA44-97D0-4D97-8339-62FD41930A3C"),
                Email = "test2@test2.com",
                PasswordSalt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                PasswordHash = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                FirstName = "Tom",
                LastName = "Cruise",
                PhoneNumber = "123456789",
                PasswordResetKey = "6968DB0F-6059-4B23-865A-317D70F46268",
                PasswordResetKeyCreatedDate = DateTimeOffset.Now,
                ImageFileId = null,
                CreatedDate = DateTimeOffset.Now,
                CreatedBy = "System",
                ModifiedDate = null,
                ModifiedBy = null,
                Status = Status.Active.ToString()
            };

            var expectedPasswordHash = "7FB56B07-18E8-4F01-9FB8-5A474A59ADCC";
            var expectedPasswordSalt = "A63DAEB5-C6CB-4F63-9F36-46E1617CF6EA";

            // act
            await authDataService.ResetPassword(expectedUser.Id, expectedPasswordHash, expectedPasswordSalt);
            var actualUser = _context.User.FirstOrDefault(x => x.Id == expectedUser.Id);

            // assert
            Assert.NotNull(actualUser);
            Assert.NotNull(expectedUser);
            Assert.NotNull(expectedUser.PasswordResetKey);
            Assert.NotNull(expectedUser.PasswordResetKeyCreatedDate);
            Assert.AreNotEqual(expectedUser.PasswordHash.ToLower(), actualUser.PasswordHash.ToLower());
            Assert.AreNotEqual(expectedUser.PasswordSalt.ToLower(), actualUser.PasswordSalt.ToLower());
            Assert.AreEqual(expectedPasswordHash.ToLower(), actualUser.PasswordHash.ToLower());
            Assert.AreEqual(expectedPasswordSalt.ToLower(), actualUser.PasswordSalt.ToLower());
            Assert.Null(actualUser.PasswordResetKey);
            Assert.Null(actualUser.PasswordResetKeyCreatedDate);
            Assert.NotNull(actualUser.ModifiedDate);
            Assert.AreEqual(DateTimeOffset.Now.Date, actualUser.ModifiedDate.Value.Date);
            Assert.NotNull(actualUser.ModifiedBy);
            Assert.AreEqual(actualUser.Email, actualUser.ModifiedBy);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase("     ")]
        [TestCase(" ")]
        public void Should_Not_Reset_Password_And_Should_Throw_Exception_When_Hashed_Password_Is_Null_Or_Empty(string hashedPassword)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);
            var expectedPasswordSalt = "A63DAEB5-C6CB-4F63-9F36-46E1617CF6EA";
            var expectedUserId = 2;

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await authDataService.ResetPassword(expectedUserId, hashedPassword, expectedPasswordSalt));
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase("     ")]
        [TestCase(" ")]
        public void Should_Not_Reset_Password_And_Should_Throw_Exception_When_Password_Salt_Is_Null_Or_Empty(string passwordSalt)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);
            var expectedPasswordHash = "A63DAEB5-C6CB-4F63-9F36-46E1617CF6EA";
            var expectedUserId = 2;

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await authDataService.ResetPassword(expectedUserId, expectedPasswordHash, passwordSalt));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(111122112)]
        [TestCase(7)]
        public void Should_Not_Reset_Password_And_Should_Throw_Exception_When_User_Does_Not_Exist(long userId)
        {
            // arrange
            var authDataService = new AuthDataService(_mapper, _context);
            var expectedPasswordHash = "7FB56B07-18E8-4F01-9FB8-5A474A59ADCC";
            var expectedPasswordSalt = "A63DAEB5-C6CB-4F63-9F36-46E1617CF6EA";

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await authDataService.ResetPassword(userId, expectedPasswordHash, expectedPasswordSalt));
        }
    }
}
