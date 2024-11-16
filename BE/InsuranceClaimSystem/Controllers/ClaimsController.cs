using InsuranceClaimSystem.DTOs.Claim.Request;
using InsuranceClaimSystem.Services.Claim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaimSystem.Controllers
{
    [Route("api/insurance-claims")]
    [ApiController]
    [Authorize]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimsController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetClaimRequest request)
        {
            var res = await _claimService.GetClaimsAsync(request);

            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var res = await _claimService.GetClaimByIdAsync(id);

            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] UpSertClaimRequest request)
        {
            var res = await _claimService.CreateClaimAsync(request);

            return Ok(res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Constants.Roles.ROLE_USER)]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UpSertClaimRequest request)
        {
            var res = await _claimService.UpdateClaimAsync(id, request);

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var res = await _claimService.DeleteClaimAsync(id);

            return Ok(res);
        }

        [HttpPut("{id}/process")]
        [Authorize(Roles = Constants.Roles.ROLE_ADMIN)]
        public async Task<IActionResult> Process([FromRoute] string id)
        {
            var res = await _claimService.ProcessClaimAsync(id);

            return Ok(res);
        }
    }
}
