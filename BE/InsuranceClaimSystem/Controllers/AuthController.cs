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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var res = await _authService.LoginAsync(request);

            return Ok(res);
        }
    }
}
