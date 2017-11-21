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

        public async Task<User> GetUserByEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var user = await _context.User
                    .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()) &&
                                              (!x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower())));

                return _mapper.Map<ClawLibrary.Data.Models.User, User>(user);
            }
            throw new BusinessException(ErrorCode.InvalidValue, "User email is null or empty");
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

        public async Task<User> RegisterUser(User model, string hashedPassword, string salt)
        {
            var user = await _context.User.FirstOrDefaultAsync(
                x => (x.Email.ToString().ToLower().Equals(model.Email.ToLower())) && (
                         !x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower())));

            if (user == null)
            {
                user = _mapper.Map<User, Models.User>(model);

                user.CreatedDate = DateTimeOffset.Now;
                user.CreatedBy = user.Email;
                user.PasswordHash = hashedPassword;
                user.PasswordSalt = salt;
                user.Key = Guid.NewGuid();
                user.Status = Status.Pending.ToString();

                var createdUser = await _context.User.AddAsync(user);

                var roles = await _context.Role.Where(x => x.Status == Status.Active.ToString())
                    .ToListAsync();

                var lastId = await _context.UserRole.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();

                foreach (var role in roles)
                {
                    lastId++;
                    var userRole = new UserRole
                    {
                        Id = lastId > 0 ? lastId + 1 : 1,
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
            throw new BusinessException(ErrorCode.AlreadyExist, $"User with email {user.Email} already exist!");
        }

        public async Task VerifyUser(long userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId && (!x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower())));

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
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId && (!x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower())));

            if (user == null)
                throw new BusinessException(ErrorCode.UserDoesNotExist);

            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new BusinessException(ErrorCode.CannotBeNullOrEmpty, "Wrong password hashed");

            if (string.IsNullOrWhiteSpace(salt))
                throw new BusinessException(ErrorCode.CannotBeNullOrEmpty, "Wrong password salt");

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
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId && (!x.Status.ToLower().Equals(Status.Deleted.ToString().ToLower())));

            if (user == null)
                throw new BusinessException(ErrorCode.UserDoesNotExist);

            if(string.IsNullOrWhiteSpace(passwordResetKey))
                throw new BusinessException(ErrorCode.CannotBeNullOrEmpty, "Wrong password reset key");

            user.ModifiedDate = DateTimeOffset.Now;
            user.ModifiedBy = user.Email;
            user.PasswordResetKey = passwordResetKey;
            user.PasswordResetKeyCreatedDate = DateTimeOffset.Now;

            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
