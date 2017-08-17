using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClawLibrary.Core.Models;
using ClawLibrary.Services.ApiServices;
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

        // GET: api/users/
        /// <summary>
        /// Gets an authenticated user for the current login. 
        /// </summary>
        /// <returns>User details</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var item = await _apiService.GetUserByKey();
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // PUT api/users/
        /// <summary>
        /// Updates the authenticated user.
        /// </summary>
        /// <param name="model">User data</param>
        /// <returns>User details</returns>
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Put([FromBody] UserRequest model)
        {
            var item = await _apiService.UpdateUser(model);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // PATCH api/users/picture
        /// <summary>
        /// Updates the user picture for authenticated user
        /// </summary>
        /// <param name="request">Picture base64 string</param>
        /// <returns>User picture</returns>
        [HttpPatch]
        [Route("picture")]
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
        [Route("picture")]
        public async Task<IActionResult> Picture()
        {
            Media media = await _apiService.GetPicture();
            if (media == null)
            {
                return NotFound();
            }
            return File(media.Content, "image/jpeg");
        }
    }
}
