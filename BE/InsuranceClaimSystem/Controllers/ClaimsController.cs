using InsuranceClaimSystem.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaimSystem.Controllers
{
    [Route("api/insurance-claims")]
    [ApiController]
    //[Authorize]
    public class ClaimsController : ControllerBase
    {
        public ClaimsController()
        {

        }

        [HttpGet("index")]
        public IActionResult UnAuthIndex()
        {
            return Ok("UnAuthIndex");
        }

        [HttpGet("auth/index")]
        [Authorize(Roles = Roles.ROLE_ADMIN)]
        public IActionResult AuthIndex()
        {
            return Ok("AuthIndex");
        }
    }
}
