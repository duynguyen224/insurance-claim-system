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

        [HttpGet("/api/public/insurance-claims")]
        public IActionResult UnAuthIndex()
        {
            return Ok("UnAuthIndex");
        }

        [HttpGet("")]
        [Authorize(Roles = Roles.ROLE_ADMIN)]
        public IActionResult AuthIndex()
        {
            return Ok("AuthIndex");
        }
    }
}
