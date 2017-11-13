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
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ClawLibrary.Data.UnitTests.DataServicesTests
{
    [TestFixture]
    public class UsersDataServiceUnitTests
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        private List<User> _data;

        public UsersDataServiceUnitTests()
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
                    ModifiedDate = new DateTimeOffset(new DateTime(2017,9,11)),
                    ModifiedBy = "aSystem",
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
                    CreatedDate = new DateTimeOffset(new DateTime(2017,9,9)),
                    CreatedBy = "aSystem",
                    ModifiedBy = "aSystem",
                    ModifiedDate = new DateTimeOffset(new DateTime(2017,9,10)),
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
                    FirstName = "Omarek",
                    LastName = "Hemsworth",
                    PhoneNumber = "123456789",
                    PasswordResetKey = null,
                    PasswordResetKeyCreatedDate = null,
                    ImageFileId = null,
                    CreatedDate = new DateTimeOffset(new DateTime(2017,9,11)),
                    CreatedBy = "cSystem",
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
                    ModifiedDate = new DateTimeOffset(new DateTime(2017,9,12)),
                    ModifiedBy = "zSystem",
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
                    Key = new Guid("8EFA1076-7905-45DB-A8CB-CDEB792C1442"),
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
                    ModifiedDate = new DateTimeOffset(new DateTime(2017,9,12)),
                    ModifiedBy = "zSystem",
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

        [TestCase("9955C5CA-8BAA-4C69-8E9A-AA17AE51138E", 1)]
        [TestCase("E9FCFA44-97D0-4D97-8339-62FD41930A3C", 2)]
        [TestCase("9CE78CDA-6366-43CC-B746-E9D0279C89DB", 5)]
        [TestCase("8EFA1076-7905-45DB-A8CB-CDEB792C1442", 6)]
        public async Task Should_Return_User_With_Specified_Key(string userKey, int expectedId)
        {
            // arrange
            var usersDataService = new UsersDataService(_mapper, _context);

            // act
            var user = await usersDataService.GetUserByKey(userKey);

            // assert
            Assert.NotNull(user);
            Assert.AreEqual(expectedId, user.Id);
        }

        [TestCase("wronguserKey")]
        [TestCase("    ")]
        [TestCase("123123213")]
        public async Task Should_Return_Null_When_User_Key_Is_Wrong(string userKey)
        {
            // arrange
            var usersDataService = new UsersDataService(_mapper, _context);

            // act
            var user = await usersDataService.GetUserByKey(userKey);

            // assert
            Assert.Null(user);
        }

        [Test]
        public void Should_Throw_Exception_When_User_Key_Is_Null()
        {
            // arrange
            var usersDataService = new UsersDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await usersDataService.GetUserByKey(null));
        }

        [Test]
        public async Task Should_Return_Null_When_User_Status_Is_Deleted()
        {
            // arrange
            var usersDataService = new UsersDataService(_mapper, _context);

            // act
            var user = await usersDataService.GetUserByKey("8BEB61E6-50CA-417E-9784-690E32B905F6");

            // assert
            Assert.Null(user);
        }

        [Test]
        public void Should_Throw_Exception_When_User_Key_Is_Empty()
        {
            // arrange
            var usersDataService = new UsersDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await usersDataService.GetUserByKey(string.Empty));
        }

        [Test]
        public async Task Should_Return_Users_With_Default_Order()
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);
            var expectedTotalCount = _data.Count(x => !x.Key.ToString().ToLower().Equals("9955C5CA-8BAA-4C69-8E9A-AA17AE51138E".ToLower()) &&
                                                                  !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));
            var expectedFirstItemId = 2;

            // act
            var users = await dataService.GetUsers("9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",null, null, String.Empty, String.Empty);

            // assert
            Assert.NotNull(users.TotalCount);
            Assert.NotNull(users.Items);
            Assert.AreEqual(expectedTotalCount, users.TotalCount);
            Assert.AreEqual(expectedTotalCount, users.Items.Length);
            Assert.AreEqual(expectedFirstItemId, users.Items[0].Id);
        }

        [TestCase("email_asc", 2)]
        [TestCase("firstname_asc", 4)]
        [TestCase("lastname_asc", 4)]
        [TestCase("phonenumber_asc", 2)]
        [TestCase("createddate_asc", 2)]
        [TestCase("modifieddate_asc", 3)]
        [TestCase("email_desc", 6)]
        [TestCase("firstname_desc", 2)]
        [TestCase("lastname_desc", 5)]
        [TestCase("phonenumber_desc", 2)]
        [TestCase("createddate_desc", 4)]
        [TestCase("modifieddate_desc", 4)]
        public async Task Should_Return_Users_With_Specified_Order(string order, long expectedFirstItemId)
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);

            // act
            var users = await dataService.GetUsers("9955C5CA-8BAA-4C69-8E9A-AA17AE51138E", null, null, order, String.Empty);

            // assert
            Assert.NotNull(users.TotalCount);
            Assert.NotNull(users.Items);
            Assert.AreEqual(expectedFirstItemId, users.Items[0].Id);
        }

        [TestCase("email_asc", 3)]
        [TestCase("firstname_asc", 5)]
        [TestCase("lastname_asc", 3)]
        [TestCase("phonenumber_asc", 3)]
        [TestCase("createddate_asc", 3)]
        [TestCase("modifieddate_asc", 3)]
        [TestCase("email_desc", 5)]
        [TestCase("firstName_desc", 3)]
        [TestCase("lastName_desc", 5)]
        [TestCase("phonenumber_desc", 3)]
        [TestCase("createddate_desc", 5)]
        [TestCase("modifieddate_desc", 3)]
        public async Task Should_Return_Users_With_Specified_Order_Which_Contains_Text(string order, long expectedFirstItemId)
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);
            var expectedTotalCount = 2;
            var expectedItemsCount = 2;
            var searchString = "Omar";

            // act
            var User = await dataService.GetUsers("9955C5CA-8BAA-4C69-8E9A-AA17AE51138E", null, null, order, searchString);

            // assert
            Assert.NotNull(User.TotalCount);
            Assert.NotNull(User.Items);
            Assert.AreEqual(expectedTotalCount, User.TotalCount);
            Assert.AreEqual(expectedItemsCount, User.Items.Length);
            Assert.AreEqual(expectedFirstItemId, User.Items[0].Id);
        }

        [TestCase(1, 0, 2)]
        [TestCase(1, 1, 3)]
        [TestCase(1, 2, 4)]
        [TestCase(1, 3, 5)]
        [TestCase(1, 4, 6)]
        public async Task Should_Return_Users_With_Offset_And_Count(int count, int offset, int expectedFirstItemId)
        {
            // arrange
            var usersDataService = new UsersDataService(_mapper, _context);
            var expectedTotalCount = 5;
            var expectedItemsCount = 1;

            // act
            var user = await usersDataService.GetUsers("9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",count, offset, String.Empty, String.Empty);

            // assert
            Assert.NotNull(user.TotalCount);
            Assert.NotNull(user.Items);
            Assert.AreEqual(expectedTotalCount, user.TotalCount);
            Assert.AreEqual(expectedItemsCount, user.Items.Length);
            Assert.AreEqual(expectedFirstItemId, user.Items[0].Id);
        }

        [Test]
        public async Task Should_Update_User()
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);
            var expectedModifiedByKey = "E9FCFA44-97D0-4D97-8339-62FD41930A3C";
            var expectedModifiedBy = "test2@test2.com";
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 1,
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "test@test.com",
                FirstName = "Rowan",
                LastName = "Atkinson",
                PhoneNumber = "516851451",
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = DateTimeOffset.Now,
                Status = Status.Active.ToString(),

            };
           
            // act
            var actualUser = await dataService.Update(expectedUser, expectedModifiedByKey);

            // assert
            Assert.NotNull(actualUser);
            Assert.AreEqual(expectedUser.Id, actualUser.Id);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
            Assert.AreEqual(expectedUser.PhoneNumber, actualUser.PhoneNumber);
            Assert.AreEqual(expectedUser.CreatedDate.Date, actualUser.CreatedDate.Date);
            Assert.NotNull(actualUser.ModifiedDate);
            Assert.AreEqual(expectedUser.ModifiedDate?.Date, actualUser.ModifiedDate?.Date);
            Assert.AreEqual(expectedUser.Status, actualUser.Status);
            Assert.AreEqual(expectedModifiedBy, actualUser.ModifiedBy);
            Assert.AreEqual(expectedUser.Salt, actualUser.Salt);
        }

        [TestCase("wrongUserKey")]
        [TestCase("    ")]
        [TestCase("123123213")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("8BEB61E6-50CA-417E-9784-690E32B905F6")]
        public void Should_Not_Update_User_And_Should_Throw_Exception_When_User_Key_Is_Null(string userKey)
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);
            var expectedModifiedByKey = "E9FCFA44-97D0-4D97-8339-62FD41930A3C";
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 1,
                Key = userKey,
                Email = "test@test.com",
                FirstName = "Rowan",
                LastName = "Atkinson",
                PhoneNumber = "516851451",
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Pending.ToString(),

            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await dataService.Update(expectedUser, expectedModifiedByKey));
        }

        [TestCase("wrongUserKey")]
        [TestCase("    ")]
        [TestCase("123123213")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("8BEB61E6-50CA-417E-9784-690E32B905F6")]
        public void Should_Not_Update_User_And_Should_Throw_Exception_When_Modifier_User_Key_Is_Null(string expectedModifiedByKey)
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);
            var expectedUser = new Core.Models.Users.User()
            {
                Id = 1,
                Key = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E",
                Email = "test@test.com",
                FirstName = "Rowan",
                LastName = "Atkinson",
                PhoneNumber = "516851451",
                PasswordResetKey = null,
                Salt = "1b568a7c-61cf-415c-b293-dcf40362192c",
                Password = "AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==",
                PasswordResetKeyCreatedDate = null,
                CreatedDate = DateTimeOffset.Now,
                ModifiedDate = null,
                Status = Status.Pending.ToString(),

            };

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await dataService.Update(expectedUser, expectedModifiedByKey));
        }

        [TestCase("wrongKey")]
        [TestCase("    ")]
        [TestCase("a123123213")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("8BEB61E6-50CA-417E-9784-690E32B905F6")]
        public void Should_Not_Return_User_Picture_And_Should_Throw_Exception_When_User_Key_Is_Wrong(string expectedUserKey)
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);

            // act & assert
            Assert.ThrowsAsync<BusinessException>(async () => await dataService.GetPicture(expectedUserKey));
        }

        [Test]
        public async Task Should_Return_File_Name_Of_User_Picture()
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);
            var expectedUserKey = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E";
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
            var actualFileName = await dataService.GetPicture(expectedUserKey);

            // assert
            Assert.NotNull(actualFileName);
            Assert.IsNotEmpty(actualFileName);
            Assert.AreEqual(expectedFile.FileName, actualFileName);
        }

        [Test]
        public async Task Should_Return_Empty_String_When_User_Picture_Does_Not_Exist()
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);
            var expectedUserKey = "E9FCFA44-97D0-4D97-8339-62FD41930A3C";

            // act
            var actualFileName = await dataService.GetPicture(expectedUserKey);

            // assert
            Assert.AreEqual(String.Empty, actualFileName);
        }

        [Test]
        public async Task Should_Create_New_User_Picture()
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);
            var expectedUserKey = "2DBF6780-9500-427E-A9C5-F843611D0BBC";
            var expectedFile = new File()
            {
                FileName = "test2FileName",
                CreatedBy = "test3@test3.com",
                CreatedDate = DateTimeOffset.Now,
                Status = Status.Active.ToString()
            };

            // act
            var isFileExisting =
                await _context.File.AnyAsync(
                    x => x.FileName.ToLower().Equals(expectedFile.FileName.ToLower()));

            await dataService.UpdatePicture(expectedFile.FileName, expectedUserKey);

            var actualFile = await _context.File.FirstOrDefaultAsync(x => x.FileName.ToLower().Equals(expectedFile.FileName.ToLower()));

            var user =
                await _context.User.Include("ImageFile").FirstOrDefaultAsync(
                    x => x.Key.ToString().ToLower().Equals(expectedUserKey.ToLower()));

            // assert
            Assert.NotNull(actualFile);
            Assert.False(isFileExisting);
            Assert.NotNull(user.ImageFile);
            Assert.AreEqual(expectedFile.FileName, actualFile.FileName);
            Assert.AreEqual(expectedFile.CreatedBy, actualFile.CreatedBy);
            Assert.AreEqual(expectedFile.CreatedDate.Date, actualFile.CreatedDate.Date);
            Assert.AreEqual(expectedFile.Status, actualFile.Status);

        }

        [Test]
        public async Task Should_Update_User_Picture()
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);
            var expectedUserKey = "9955C5CA-8BAA-4C69-8E9A-AA17AE51138E";
            var expectedFile = new File()
            {
                FileName = "test2FileName",
                ModifiedBy = "test@test.com",
                ModifiedDate = DateTimeOffset.Now,
                Status = Status.Active.ToString()
            };


            // act
            var isFileExisting =
                await _context.File.AnyAsync(
                    x => x.FileName.ToLower().Equals(expectedFile.FileName.ToLower()));

            var oldFileName = _context.User.Include("ImageFile").FirstOrDefault(
                    x => x.Key.ToString().ToLower().Equals(expectedUserKey.ToLower()))
                ?.ImageFile.FileName;


            await dataService.UpdatePicture(expectedFile.FileName, expectedUserKey);

            var actualFile = await _context.File.FirstOrDefaultAsync(x => x.FileName.ToLower().Equals(expectedFile.FileName.ToLower()));

            var user =
                await _context.User.Include("ImageFile").FirstOrDefaultAsync(
                    x => x.Key.ToString().ToLower().Equals(expectedUserKey.ToLower()));

            // assert
            Assert.NotNull(actualFile);
            Assert.False(String.IsNullOrEmpty(oldFileName));
            Assert.AreNotEqual(oldFileName, actualFile.FileName);
            Assert.False(isFileExisting);
            Assert.NotNull(user.ImageFile);
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
        public void Should_Not_Update_User_Picture_And_Should_Throw_Exception_When_User_Key_Is_Wrong(string expectedUserKey)
        {
            // arrange
            var dataService = new UsersDataService(_mapper, _context);

            // act & assert
                Assert.ThrowsAsync<BusinessException>(async () => await dataService.UpdatePicture(String.Empty, expectedUserKey));
        }
    }
}
