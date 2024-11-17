using InsuranceClaimSystem.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaimSystem.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <remarks>Get users</remarks>
        /// <response code="200">Get data successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _userService.GetUsersAsync();

            return StatusCode(res.StatusCode, res);
        }
    }
}
