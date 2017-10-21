using System.Collections.Generic;
using System.Threading.Tasks;
using ClawLibrary.Core.Models;
using ClawLibrary.Core.Models.Users;

namespace ClawLibrary.Core.DataServices
{
    /// <summary>
    /// User data service
    /// </summary>
    public interface IUsersDataService
    {
        /// <summary>
        /// Get authenticated user.
        /// </summary>
        /// <param name="userKey">Key of the current user</param>
        /// <returns>User details</returns>
        Task<User> GetByKey(string userKey);

        /// <summary>
        /// Get a list of the users.
        /// </summary>
        /// <param name="userKey">Key of the current user</param>
        /// <param name="count">Limit the rows returned to a specified range</param>
        /// <param name="offset">Offset of rows returned</param>
        /// <param name="orderBy">Order the result set of a query by the specified column</param>
        /// <param name="searchString">User first name or last name or email</param>
        /// <returns>List of users</returns>
        Task<ListResponse<User>> GetUsers(string userKey, int? count, int? offset, string orderBy, string searchString);

        /// <summary>
        /// Updates authenticated user.
        /// </summary>
        /// <param name="model">User model</param>
        /// <param name="userKey">Key of the current user</param>
        /// <returns>User details</returns>
        Task<User> Update(User model, string userKey);

        /// <summary>
        /// Gets the picture for user.
        /// </summary>
        /// <param name="userKey">Key of the current user</param>
        /// <returns>picture</returns>
        Task<string> GetPicture(string userKey);

        /// <summary>
        /// Updates the user picture.
        /// </summary>
        /// <param name="fileName">Picture name</param>
        /// <param name="userKey">Key of the current user</param>
        Task UpdatePicture(string fileName, string userKey);
    }
}