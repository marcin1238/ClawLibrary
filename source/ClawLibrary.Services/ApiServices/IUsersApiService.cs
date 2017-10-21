using System.Threading.Tasks;
using ClawLibrary.Core.Models;
using ClawLibrary.Services.Models.Users;

namespace ClawLibrary.Services.ApiServices
{
    /// <summary>
    /// Users api service
    /// </summary>
    public interface IUsersApiService
    {
        /// <summary>
        /// Gets an authenticated user for the current login. 
        /// </summary>
        /// <returns>User details response</returns>
        Task<UserResponse> GetAuthenticatedUser();

        /// <summary>
        /// Gets an user with specified key (if one is present).
        /// </summary>
        /// <param name="userKey">Key of the user</param>
        /// <returns>User details response</returns>
        Task<UserResponse> GetUserByKey(string userKey);

        /// <summary>
        /// Get a list of the users.
        /// </summary>
        /// <param name="query"> Limit the rows returned to a specified range</param>
        /// <returns>List of users response</returns>
        Task<ListResponse<UserResponse>> GetUsers(QueryData query);

        /// <summary>
        /// Updates the authenticated user.
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>User details response</returns>
        Task<UserResponse> UpdateUser(UserRequest model);

        /// <summary>
        /// Updates the user picture for authenticated user.
        /// </summary>
        /// <param name="requestPictureBase64">Picture base64 string</param>
        /// <returns>User picture response</returns>
        Task<Media> UpdatePicture(string requestPictureBase64);

        /// <summary>
        /// Gets picture for authenticated user.
        /// </summary>
        /// <returns>User picture response</returns>
        Task<Media> GetPicture();

        /// <summary>
        /// Updates the user picture.
        /// </summary>
        /// <param name="requestPictureBase64">Picture base64 string</param>
        /// <param name="userKey">Key of the user</param>
        /// <returns>User picture response</returns>
        Task<Media> UpdatePicture(string requestPictureBase64, string userKey);

        /// <summary>
        /// Gets picture of user with specified key (if one is present)..
        /// </summary>
        /// <param name="userKey">Key of the user</param>
        /// <returns>User picture response</returns>
        Task<Media> GetPicture(string userKey);
    }
}