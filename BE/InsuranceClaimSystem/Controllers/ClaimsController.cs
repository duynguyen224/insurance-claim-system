using InsuranceClaimSystem.DTOs.Claim;
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

        /// <summary>
        /// Get claims
        /// </summary>
        /// <remarks>Get claims. (User can only get their claims. Admin can get whatever claims)</remarks>
        /// <response code="200">Get data successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetClaimRequest request)
        {
            var res = await _claimService.GetClaimsAsync(request);

            return StatusCode(res.StatusCode, res);
        }

        /// <summary>
        /// Get claim by id
        /// </summary>
        /// <remarks>Get a claim by its id. (User can only get their claim. Admin can get whatever claim)</remarks>
        /// <response code="200">Get data successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Claim not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var res = await _claimService.GetClaimByIdAsync(id);

            return StatusCode(res.StatusCode, res);
        }

        /// <summary>
        /// Create a new claim
        /// </summary>
        /// <remarks>Create a new claim (anonymous and user role or admin role can perform this action)</remarks>
        /// <response code="201">Create claim successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] UpSertClaimRequest request)
        {
            var res = await _claimService.CreateClaimAsync(request);

            return StatusCode(res.StatusCode, res);
        }

        /// <summary>
        /// Update claim
        /// </summary>
        /// <remarks>Update claim (only user that create the claim can perform this action)</remarks>
        /// <response code="201">Update claim successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="403">Claim not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [Authorize(Roles = Constants.Roles.ROLE_USER)]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] UpSertClaimRequest request)
        {
            var res = await _claimService.UpdateClaimAsync(id, request);

            return StatusCode(res.StatusCode, res);
        }

        /// <summary>
        /// Delete claim
        /// </summary>
        /// <remarks>Delete claim (only user that create the claim and admin can perform this action)</remarks>
        /// <response code="201">Delete claim successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Claim not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var res = await _claimService.DeleteClaimAsync(id);

            return StatusCode(res.StatusCode, res);
        }

        /// <summary>
        /// Process claim
        /// </summary>
        /// <remarks>Process claim (only admin can perform this action)</remarks>
        /// <response code="201">Delete claim successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="403">Claim not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}/process")]
        [Authorize(Roles = Constants.Roles.ROLE_ADMIN)]
        public async Task<IActionResult> Process([FromRoute] string id)
        {
            var res = await _claimService.ProcessClaimAsync(id);

            return StatusCode(res.StatusCode, res);
        }
    }
}
