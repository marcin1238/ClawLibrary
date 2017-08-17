using System.Threading.Tasks;
using ClawLibrary.Core.Models;
using ClawLibrary.Services.Models.Users;

namespace ClawLibrary.Services.ApiServices
{
    /// <summary>
    /// User api service
    /// </summary>
    public interface IUsersApiService
    {
        /// <summary>
        /// Gets an authenticated user for the current login. 
        /// </summary>
        /// <returns>User details</returns>
        Task<UserResponse> GetUserByKey();

        /// <summary>
        /// Updates the authenticated user.
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>User details</returns>
        Task<UserResponse> UpdateUser(UserRequest model);

        /// <summary>
        /// Updates the user picture.
        /// </summary>
        /// <param name="requestPictureBase64">Picture base64 string</param>
        /// <returns>User picture</returns>
        Task<Media> UpdatePicture(string requestPictureBase64);

        /// <summary>
        /// Gets picture for authenticated user.
        /// </summary>
        /// <returns>User picture</returns>
        Task<Media> GetPicture();
    }
}