using System.Threading.Tasks;
using ClawLibrary.Core.Models.Auth;
using ClawLibrary.Services.ApiServices;
using Microsoft.AspNetCore.Mvc;

namespace ClawLibrary.Web.Controllers
{
    /// <summary>
    /// Authorization controller.
    /// </summary>
    [Route("api/")]
    public class AuthorizationController : Controller
    {
        private readonly IAuthApiService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationController"/> class.
        /// </summary>
        /// <param name="authService">Authorization service</param>
        public AuthorizationController(IAuthApiService authService)
        {
            _authService = authService;
        }

        // POST: api/{appName}/token
        /// <summary>
        /// Creates the token for the specified user if user information are valid.
        /// </summary>
        /// <param name="request">User data</param>
        /// <returns>Token</returns>
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Post([FromBody] AuthorizeRequest request)
        {
            var item = await _authService.Authorize(request);
            if (item == null)
                return Unauthorized();

            return new ObjectResult(item);
        }

        // POST: api/{appName}/verify
        /// <summary>
        /// Verify user.
        /// </summary>
        /// <param name="request">Verification information</param>
        /// <returns>Token</returns>
        [HttpPost]
        [Route("verify")]
        public async Task<IActionResult> Post([FromBody] UserVerificationRequest request)
        {
            var item = await _authService.VerifyUser(request);
            if (item == null)
                return Unauthorized();

            return new ObjectResult(item);
        }

        // POST: api/password/reset
        /// <summary>
        /// Reset user password.
        /// </summary>
        /// <param name="request">Password data</param>
        [HttpPost]
        [Route("password/reset")]
        public async Task<IActionResult> Post([FromBody] ResetUserPasswordRequest request)
        {
            await _authService.ResetPassword(request);

            return new NoContentResult();
        }

        // POST: api/password/set
        /// <summary>
        /// Set new user password.
        /// </summary>
        /// <param name="request">Password data</param>
        [HttpPost]
        [Route("password/set")]
        public async Task<IActionResult> Post([FromBody] SetUserPasswordRequest request)
        {
            await _authService.SetPassword(request);

            return new NoContentResult();
        }

        // POST: api/register
        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="request">User data</param>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserRequest request)
        {
            await _authService.RegisterUser(request);
            return new NoContentResult();
        }
    }
}
