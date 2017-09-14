using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
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

        public async Task<User> GetByKey(string userKey)
        {
            var user = await _context.User
                .FirstAsync(x => x.Key.ToString() == userKey && x.Status.ToLower().Equals(Status.Active.ToString().ToLower()));

            return _mapper.Map<ClawLibrary.Data.Models.User, User>(user);
        }

        public async Task<List<User>> GetUsers(string userKey, int? count, int? offset, string orderBy, string searchString)
        {
            if(string.IsNullOrWhiteSpace(searchString))
                switch (orderBy.ToLower())
                {
                    case "email_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x=>!x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Email)
                            .ToListAsync());
                    case "firstname_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.FirstName)
                            .ToListAsync());
                    case "lastname_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.LastName)
                            .ToListAsync());
                    case "phonenumber_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.PhoneNumber)
                            .ToListAsync());
                    case "createddate_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.CreatedDate)
                            .ToListAsync());
                    case "modifieddate_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.ModifiedDate)
                            .ToListAsync());
                    case "email_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Email)
                            .ToListAsync());
                    case "firstName_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.FirstName)
                            .ToListAsync());
                    case "lastName_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.LastName)
                            .ToListAsync());
                    case "phonenumber_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.PhoneNumber)
                            .ToListAsync());
                    case "createddate_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.CreatedDate)
                            .ToListAsync());
                    case "modifieddate_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x=>x.ModifiedDate)
                            .ToListAsync());
                    default:
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .ToListAsync());
                }
            else
                switch (orderBy.ToLower())
                {
                    case "email_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) &&  (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                        x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                        x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.Email)
                            .ToListAsync());
                    case "firstname_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.FirstName)
                            .ToListAsync());
                    case "lastname_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.LastName)
                            .ToListAsync());
                    case "phonenumber_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.PhoneNumber)
                            .ToListAsync());
                    case "createddate_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.CreatedDate)
                            .ToListAsync());
                    case "modifieddate_asc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderBy(x => x.ModifiedDate)
                            .ToListAsync());
                    case "email_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.Email)
                            .ToListAsync());
                    case "firstName_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.FirstName)
                            .ToListAsync());
                    case "lastName_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.LastName)
                            .ToListAsync());
                    case "phonenumber_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.PhoneNumber)
                            .ToListAsync());
                    case "createddate_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.CreatedDate)
                            .ToListAsync());
                    case "modifieddate_desc":
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .OrderByDescending(x => x.ModifiedDate)
                            .ToListAsync());
                    default:
                        return _mapper.Map<List<ClawLibrary.Data.Models.User>, List<User>>(await _context
                            .User
                            .Where(x => !x.Key.ToString().ToLower().Equals(userKey.ToLower()) && (x.Email.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                                                                                  x.LastName.ToLower().Contains(searchString.ToLower())))
                            .Skip(offset ?? 0)
                            .Take(count ?? 100)
                            .ToListAsync());
                }
        }

        public async Task<User> Update(User model, string userKey)
        {
            var user = await _context.User.FirstAsync(x => x.Key.ToString() == userKey && x.Status == Status.Active.ToString());

            if (user == null)
                throw new BusinessException(ErrorCode.UserDoesNotExist);

            user.ModifiedDate = DateTimeOffset.Now;
            user.ModifiedBy = user.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            var updateUser = _context.User.Update(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClawLibrary.Data.Models.User, User>(updateUser.Entity);
        }

        public async Task<string> GetPicture(string userKey)
        {
            var user = await _context.User.Include("ImageFile").FirstAsync(x => x.Key.ToString() == userKey && x.Status == Status.Active.ToString());

            if (user == null)
                throw new BusinessException(ErrorCode.UserDoesNotExist);

            if (user.ImageFile == null)
                return string.Empty;
            return user.ImageFile.FileName;
        }

        public async Task UpdatePicture(string fileName, string userKey)
        {
            File file;
            var user = await _context.User.Include("ImageFile").FirstAsync(x => x.Key.ToString() == userKey && x.Status == Status.Active.ToString());

            if (user == null)
                throw new BusinessException(ErrorCode.UserDoesNotExist);

            if (user.ImageFile == null)
            {
                file = new File();
                file.FileName = fileName;
                file.Key = Guid.NewGuid();
                file.CreatedBy = user.Email;
                file.CreatedDate = DateTimeOffset.Now;
                file.Status = Status.Active.ToString();
            }
            else
            {
                file = user.ImageFile;
                file.FileName = fileName;
                file.ModifiedBy = user.Email;
                file.ModifiedDate = DateTimeOffset.Now;
            }

            user.ImageFile = file;
            user.ModifiedBy = user.Email;
            user.ModifiedDate = DateTimeOffset.Now;

            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
       
    }

}
