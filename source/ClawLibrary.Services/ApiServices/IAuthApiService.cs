using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClawLibrary.Core.Models.Auth;
using AuthorizeRequest = ClawLibrary.Services.Models.Users.AuthorizeRequest;
using RegisterUserRequest = ClawLibrary.Services.Models.Users.RegisterUserRequest;
using ResetUserPasswordRequest = ClawLibrary.Services.Models.Users.ResetUserPasswordRequest;
using SetUserPasswordRequest = ClawLibrary.Services.Models.Users.SetUserPasswordRequest;
using UserVerificationRequest = ClawLibrary.Services.Models.Users.UserVerificationRequest;

namespace ClawLibrary.Services.ApiServices
{
    public interface IAuthApiService
    {
        /// <summary>
        /// Get authentication token for user.
        /// </summary>
        /// <param name="user">Authentication details</param>
        /// <returns>Token details</returns>
        Task<Token> Authorize(AuthorizeRequest user);

        /// <summary>
        /// Registers new user for given organization. Sends verification email to newly created user.
        /// </summary>
        /// <param name="request">New user details</param>
        Task RegisterUser(RegisterUserRequest request);

        /// <summary>
        /// Verify newly created user and authenticate user.
        /// </summary>
        /// <param name="request">User verification details</param>
        /// <returns>Token details</returns>
        Task<Token> VerifyUser(UserVerificationRequest request);

        /// <summary>
        /// Generate password reset key and send it to user.
        /// </summary>
        /// <param name="request">Reset password request details</param>
        Task ResetPassword(ResetUserPasswordRequest request);

        /// <summary>
        /// Set new password for user.
        /// </summary>
        /// <param name="request">Set password request details</param>
        Task SetPassword(SetUserPasswordRequest request);

    }
}
