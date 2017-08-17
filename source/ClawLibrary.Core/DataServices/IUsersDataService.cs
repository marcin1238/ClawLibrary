using System.Threading.Tasks;
using ClawLibrary.Core.Models;

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