using InsuranceClaimSystem.DTOs.Auth;
using InsuranceClaimSystem.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaimSystem.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <remarks>Authenticate user to make sure user has the right role in the system</remarks>
        /// <response code="200">Login successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">Invalid credentials</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var res = await _authService.LoginAsync(request);

            return StatusCode(res.StatusCode, res);
        }
    }
}
