using System.Collections.Generic;
using System.Threading.Tasks;
using ClawLibrary.Core.Enums;
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
        /// Get the user with specified key (if one is present).
        /// </summary>
        /// <param name="userKey">Key of the user</param>
        /// <returns>User details</returns>
        Task<User> GetUserByKey(string userKey);

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
        /// <param name="modifiedByKey">Key of the user which modify data</param>
        /// <returns>User details</returns>
        Task<User> Update(User model, string modifiedByKey);

        /// <summary>
        /// Updates the user status.
        /// </summary>
        /// <param name="userKey">Key of the user</param>
        /// <param name="status">New user status</param>
        /// <param name="modifiedByKey">Key of the user which modify data</param>
        /// <returns>User details</returns>
        Task<User> UpdateStatus(string userKey, Status status, string modifiedByKey);

        /// <summary>
        /// Gets the picture for user.
        /// </summary>
        /// <param name="userKey">Key of the user</param>
        /// <returns>Picture</returns>
        Task<string> GetPicture(string userKey);

        /// <summary>
        /// Updates the user picture.
        /// </summary>
        /// <param name="fileName">Picture name</param>
        /// <param name="userKey">Key of the user</param>
        Task UpdatePicture(string fileName, string userKey);
    }
}