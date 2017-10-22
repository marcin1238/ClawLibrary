using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClawLibrary.Core.DataServices;
using ClawLibrary.Core.Enums;
using ClawLibrary.Core.Exceptions;
using ClawLibrary.Core.Models.Auth;
using ClawLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using User = ClawLibrary.Core.Models.Users.User;

namespace ClawLibrary.Data.DataServices
{
    public class AuthDataService : IAuthDataService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public AuthDataService(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<User> GetUser(string email)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));

            return _mapper.Map<ClawLibrary.Data.Models.User, User>(user);
        }

        public Task<List<string>> GetUserRoles(long userId)
        {
            var roles = _context.UserRole
                .Include("Role")
                .Where(x => x.UserId == userId && x.Status == Status.Active.ToString())
                .Select(x => x.Role.Name)
                .ToListAsync();

            return roles;
        }

        public async Task<User> RegisterUser(RegisterUserRequest request, string hashedPassword, string salt)
        {
            var userKey = Guid.NewGuid();

            var user = _mapper
                .Map<RegisterUserRequest, Models.User>(request);

            user.CreatedDate = DateTimeOffset.Now;
            user.CreatedBy = user.Email;
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = salt;
            user.Key = userKey;
            user.Status = Status.Pending.ToString();

            var createdUser = await _context.User.AddAsync(user);

            var roles = await _context.Role.Where(x => x.Status == Status.Active.ToString())
                .ToListAsync();

            foreach (var role in roles)
            {
                var userRole = new UserRole
                {
                    Key = Guid.NewGuid(),
                    User = createdUser.Entity,
                    Role = role,
                    CreatedBy = "System",
                    CreatedDate = DateTimeOffset.Now,
                    Status = role.Name == Core.Enums.Role.Regular.ToString()
                        ? Status.Active.ToString()
                        : Status.Inactive.ToString(),
                };

                await _context.UserRole.AddAsync(userRole);
            }

            await _context.SaveChangesAsync();


            return _mapper.Map<ClawLibrary.Data.Models.User, User>(createdUser.Entity);
        }

        public async Task VerifyUser(long userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new BusinessException(ErrorCode.UserDoesNotExist);
            if (user.Status != Status.Pending.ToString())
                throw new BusinessException(ErrorCode.UserVerified);

            user.Status = Status.Active.ToString();
            user.ModifiedDate = DateTimeOffset.Now;
            user.ModifiedBy = user.Email;

            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task ResetPassword(long userId, string hashedPassword, string salt)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId && x.Status == Status.Active.ToString());

            if (user == null)
                throw new BusinessException(ErrorCode.UserDoesNotExist);

            user.PasswordHash = hashedPassword;
            user.PasswordSalt = salt;
            user.ModifiedDate = DateTimeOffset.Now;
            user.ModifiedBy = user.Email;
            user.PasswordResetKey = null;
            user.PasswordResetKeyCreatedDate = null;

            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task CreatePasswordResetKey(long userId, string passwordResetKey)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId && x.Status == Status.Active.ToString());

            if (user == null)
                throw new BusinessException(ErrorCode.UserDoesNotExist);

            user.ModifiedDate = DateTimeOffset.Now;
            user.ModifiedBy = user.Email;
            user.PasswordResetKey = passwordResetKey;
            user.PasswordResetKeyCreatedDate = DateTimeOffset.Now;

            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
