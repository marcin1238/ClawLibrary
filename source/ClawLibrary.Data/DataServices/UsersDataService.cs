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
using User = ClawLibrary.Core.Models.User;

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

            return _mapper.Map<ClawLibrary.Data.Models.User, ClawLibrary.Core.Models.User>(user);
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

            return _mapper.Map<ClawLibrary.Data.Models.User, ClawLibrary.Core.Models.User>(updateUser.Entity);
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
