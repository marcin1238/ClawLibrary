using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.Models;
using ClawLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using User = ClawLibrary.Core.Models.Users.User;

namespace ClawLibrary.Data.DataServices
{
    public class UsersDataService : IUsersDataService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public UsersDataService(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<User> GetUserByKey(string userKey)
        {
            if (!string.IsNullOrEmpty(userKey))
            {
                var user = await _context.User
                    .FirstOrDefaultAsync(x => x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                              (!x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower())));


                return _mapper.Map<ClawLibrary.Data.Models.User, User>(user);
            }
            throw new BusinessException(ErrorCode.InvalidValue, "User key is null or empty");
        }

        public async Task<ListResponse<User>> GetUsers(string userKey, int? count, int? offset, string orderBy,
            string searchString)
        {
            long totalCount = 0;
            List<User> list;
            if (string.IsNullOrWhiteSpace(searchString))
            {
                totalCount = await _context.User.CountAsync(
                    x => !(x.Key.ToString().ToLower().Equals(userKey.ToLower())) &&
                         !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));
                switch (orderBy.ToLower())
                {
                    case "email_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Email)
                            .ToListAsync());
                        break;
                    case "firstname_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.FirstName)
                            .ToListAsync());
                        break;
                    case "lastname_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.LastName)
                            .ToListAsync());
                        break;
                    case "phonenumber_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.PhoneNumber)
                            .ToListAsync());
                        break;
                    case "createddate_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.CreatedDate)
                            .ToListAsync());
                        break;
                    case "modifieddate_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.ModifiedDate)
                            .ToListAsync());
                        break;
                    case "email_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Email)
                            .ToListAsync());
                        break;
                    case "firstname_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.FirstName)
                            .ToListAsync());
                        break;
                    case "lastname_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.LastName)
                            .ToListAsync());
                        break;
                    case "phonenumber_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.PhoneNumber)
                            .ToListAsync());
                        break;
                    case "createddate_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.CreatedDate)
                            .ToListAsync());
                        break;
                    case "modifieddate_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.ModifiedDate)
                            .ToListAsync());
                        break;
                    default:
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .ToListAsync());
                        break;
                }
            }
            else
            {
                totalCount = await _context.User.CountAsync(
                    x => !(x.Key.ToString().ToLower().Equals(userKey.ToLower())) &&
                         (x.Email.ToLower().Contains(searchString.ToLower()) ||
                          x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                          x.LastName.ToLower().Contains(searchString.ToLower())) &&
                         !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));
                switch (orderBy.ToLower())
                {
                    case "email_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Email)
                            .ToListAsync());
                        break;
                    case "firstname_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.FirstName)
                            .ToListAsync());
                        break;
                    case "lastname_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.LastName)
                            .ToListAsync());
                        break;
                    case "phonenumber_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.PhoneNumber)
                            .ToListAsync());
                        break;
                    case "createddate_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.CreatedDate)
                            .ToListAsync());
                        break;
                    case "modifieddate_asc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.ModifiedDate)
                            .ToListAsync());
                        break;
                    case "email_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Email)
                            .ToListAsync());
                        break;
                    case "firstname_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.FirstName)
                            .ToListAsync());
                        break;
                    case "lastname_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.LastName)
                            .ToListAsync());
                        break;
                    case "phonenumber_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.PhoneNumber)
                            .ToListAsync());
                        break;
                    case "createddate_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.CreatedDate)
                            .ToListAsync());
                        break;
                    case "modifieddate_desc":
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.ModifiedDate)
                            .ToListAsync());
                        break;
                    default:
                        list = _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                        (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                         x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                         x.LastName.ToLower().Contains(searchString.ToLower())) &&
                                        !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .ToListAsync());
                        break;
                }
            }
            return new ListResponse<User>(list, totalCount);
        }

        public async Task<User> Update(User model, string modifiedByKey)
        {
            if (!string.IsNullOrWhiteSpace(model.Key) && !string.IsNullOrWhiteSpace(modifiedByKey))
            {
                var modifiedBy =
                    await _context.User.FirstOrDefaultAsync(
                        x => x.Key.ToString().ToLower().Equals(modifiedByKey.ToLower()) &&
                             !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));

                var user =
                    await _context.User.FirstOrDefaultAsync(
                        x => x.Key.ToString().ToLower().Equals(model.Key.ToLower()) &&
                             !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));

                if (user == null)
                    throw new BusinessException(ErrorCode.UserDoesNotExist);

                if (modifiedBy == null)
                    throw new BusinessException(ErrorCode.UserDoesNotExist);

                user.ModifiedDate = DateTimeOffset.Now;
                user.ModifiedBy = modifiedBy.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;

                var updateUser = _context.User.Update(user);
                await _context.SaveChangesAsync();

                return _mapper.Map<ClawLibrary.Data.Models.User, User>(updateUser.Entity);


            }
            throw new BusinessException(ErrorCode.InvalidValue, "User key is null or empty");
        }

        public async Task<User> UpdateStatus(string userKey, Status status, string modifiedByKey)
        {
            if (!string.IsNullOrWhiteSpace(userKey) && !string.IsNullOrWhiteSpace(modifiedByKey))
            {
                var modifiedBy =
                    await _context.User.FirstOrDefaultAsync(
                        x => x.Key.ToString().ToLower().Equals(modifiedByKey.ToLower()) &&
                             !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));

                var user =
                    await _context.User.FirstOrDefaultAsync(
                        x => x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                             !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower()));

                if (user == null)
                    throw new BusinessException(ErrorCode.UserDoesNotExist);

                if (modifiedBy == null)
                    throw new BusinessException(ErrorCode.UserDoesNotExist);

                user.ModifiedDate = DateTimeOffset.Now;
                user.ModifiedBy = modifiedBy.Email;
                user.Status = status.ToString();

                var updateUser = _context.User.Update(user);
                await _context.SaveChangesAsync();

                return _mapper.Map<ClawLibrary.Data.Models.User, User>(updateUser.Entity);


            }
            throw new BusinessException(ErrorCode.InvalidValue, "User key is null or empty");
        }


        public async Task<string> GetPicture(string userKey)
        {
            if (!string.IsNullOrEmpty(userKey))
            {
                var user = await _context.User.Include("ImageFile")
                    .FirstOrDefaultAsync(x => x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                              (!x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower())));

                if (user == null)
                    throw new BusinessException(ErrorCode.UserDoesNotExist);

                if (user.ImageFile == null)
                    return string.Empty;

                return user.ImageFile.FileName;
            }
            throw new BusinessException(ErrorCode.InvalidValue, "User key is null or empty");
        }

        public async Task UpdatePicture(string fileName, string userKey)
        {
            File file;
            if (!string.IsNullOrWhiteSpace(userKey))
            {
                var user = await _context.User.Include("ImageFile")
                    .FirstOrDefaultAsync(x => x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&
                                              (!x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower())));

                if (user == null)
                    throw new BusinessException(ErrorCode.UserDoesNotExist);

                if (user.ImageFile == null)
                {
                    var lastId = await _context.File.OrderByDescending(x => x.Id)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();
                    file = new File();
                    file.Id = lastId > 0 ? lastId + 1 : 1;
                    file.FileName = fileName;
                    file.Key = Guid.NewGuid();
                    file.CreatedBy = user.Email;
                    file.CreatedDate = DateTimeOffset.Now;
                    file.Status = Status.Active.ToString();
                    await _context.File.AddAsync(file);
                }
                else
                {
                    file = user.ImageFile;
                    file.FileName = fileName;
                    file.ModifiedBy = user.Email;
                    file.ModifiedDate = DateTimeOffset.Now;
                    _context.File.Update(file);
                }

                user.ImageFile = file;
                user.ModifiedBy = user.Email;
                user.ModifiedDate = DateTimeOffset.Now;

                _context.User.Update(user);
                await _context.SaveChangesAsync();
            }
            else
                throw new BusinessException(ErrorCode.InvalidValue, "USer key is null or empty");
        }

    }

}
