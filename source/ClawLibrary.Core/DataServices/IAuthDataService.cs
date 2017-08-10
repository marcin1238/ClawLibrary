using System.Collections.Generic;
using System.Threading.Tasks;
using ClawLibrary.Core.Models;
using ClawLibrary.Core.Models.Auth;

namespace ClawLibrary.Core.DataServices
{
    public interface IAuthDataService
    {
        /// <summary>
        /// Get active user for the given email.
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User</returns>
        Task<User> GetUser(string email);

        /// <summary>
        /// Get active user roles for the given user.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>User roles</returns>
        Task<List<string>> GetUserRoles(long userId);

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="request">New user details</param>
        /// <param name="hashedPassword">Hashed password</param>
        /// <param name="salt">Salt added to password</param>
        /// <returns>User details</returns>
        Task<User> RegisterUser(RegisterUserRequest request, string hashedPassword, string salt);

        /// <summary>
        /// Verify new user.
        /// </summary>
        /// <param name="userId">Id of the user to verify</param>
        Task VerifyUser(long userId);

        /// <summary>
        /// Set new password for user
        /// </summary>
        /// <param name="userId">Id of the current user</param>
        /// <param name="hashedPassword">Hashed passwors</param>
        /// <param name="salt">Salt added to password</param>
        Task ResetPassword(long userId, string hashedPassword, string salt);

        /// <summary>
        /// Set password reset key for user
        /// </summary>
        /// <param name="userId">Id of the current user</param>
        /// <param name="passwordResetKey">Key needed for resetting password</param>
        Task CreatePasswordResetKey(long userId, string passwordResetKey);
    }
}