using System.Threading.Tasks;
using ClawLibrary.Core.Models;
using ClawLibrary.Services.ApiServices;
using ClawLibrary.Services.Models;
using ClawLibrary.Services.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClawLibrary.Web.Controllers
{
    /// <summary>
    /// Users controller. Provides functionality to the api/users/ route.
    /// </summary>
    [Route("api/users")]
    [Authorize(Roles = "Regular,Admin")]
    public class UsersController : Controller
    {
        private readonly IUsersApiService _apiService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="apiService"></param>
        public UsersController(IUsersApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: api/users/user
        /// <summary>
        /// Gets an authenticated user for the current login. 
        /// </summary>
        /// <returns>User details</returns>
        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> Get()
        {
            var item = await _apiService.GetAuthenticatedUser();
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // GET: api/users/user/{userKey}
        /// <summary>
        /// Gets an user with specified key (if one is present).
        /// </summary>
        /// <returns>User details</returns>
        [HttpGet]
        [Route("user/{userKey}")]
        public async Task<IActionResult> Get(string userKey)
        {
            var item = await _apiService.GetUserByKey(userKey);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        // GET: api/users
        /// <summary>
        /// Gets a list of the users.
        /// </summary>
        /// <param name="query">Limit the rows returned to a specified range</param>
        /// <returns>List of users</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromQuery] QueryData query)
        {
            var item = await _apiService.GetUsers(query);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // PUT api/users/user
        /// <summary>
        /// Updates the authenticated user.
        /// </summary>
        /// <param name="model">User data</param>
        /// <returns>User details</returns>
        [HttpPut]
        [Route("user")]
        public async Task<IActionResult> Put([FromBody] UserRequest model)
        {
            var item = await _apiService.UpdateUser(model);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // PATCH api/users/user/picture
        /// <summary>
        /// Updates the user picture for authenticated user
        /// </summary>
        /// <param name="request">Picture base64 string</param>
        /// <returns>User picture</returns>
        [HttpPatch]
        [Route("user/picture")]
        public async Task<IActionResult> Picture([FromBody] UpdatePictureRequest request)
        {
            Media media = await _apiService.UpdatePicture(request.PictureBase64);
            if (media == null)
            {
                return NotFound();
            }
            return File(media.Content, "image/jpeg");
        }

        // GET api/users/picture
        /// <summary>
        /// Gets picture for authenticated user.
        /// </summary>
        /// <returns>User picture</returns>
        [HttpGet]
        [Route("user/picture")]
        public async Task<IActionResult> Picture()
        {
            Media media = await _apiService.GetPicture();
            if (media == null)
            {
                return NotFound();
            }
            return File(media.Content, "image/jpeg");
        }

        // PATCH api/users/user/{userKey}/picture
        /// <summary>
        /// Updates the user picture for authenticated user
        /// </summary>
        /// <param name="userKey">Key of the user</param>
        /// <param name="request">Picture base64 string</param>
        /// <returns>User picture</returns>
        [HttpPatch]
        [Route("user/{userKey}/picture")]
        public async Task<IActionResult> Picture(string userKey, [FromBody] UpdatePictureRequest request)
        {
            Media media = await _apiService.UpdatePicture(request.PictureBase64, userKey);
            if (media == null)
            {
                return NotFound();
            }
            return File(media.Content, "image/jpeg");
        }

        // GET api/users/user/{userKey}/picture
        /// <summary>
        /// Gets picture for authenticated user.
        /// </summary>
        /// <param name="userKey">Key of the user</param>
        /// <returns>User picture</returns>
        [HttpGet]
        [Route("user/{userKey}/picture")]
        public async Task<IActionResult> Picture(string userKey)
        {
            Media media = await _apiService.GetPicture(userKey);
            if (media == null)
            {
                return NotFound();
            }
            return File(media.Content, "image/jpeg");
        }
    }
}
