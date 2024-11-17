using AutoMapper;
using InsuranceClaimSystem.DTOs;
using InsuranceClaimSystem.DTOs.Claim;
using InsuranceClaimSystem.Models;
using InsuranceClaimSystem.Repositories.Claim;
using InsuranceClaimSystem.Services.User;
using ClaimModel = InsuranceClaimSystem.Models.Claim;

namespace InsuranceClaimSystem.Services.Claim
{
    public class ClaimService : IClaimService
    {
        private readonly IUserService _userService;
        private readonly IClaimRepository _claimRepository;
        private readonly IMapper _mapper;

        public ClaimService(IUserService userService,
            IClaimRepository claimRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _userService = userService;
            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<ClaimResponse>>> GetClaimsAsync(GetClaimRequest request)
        {
            // User can only get their claims, admin can get all claims of users
            var userId = _userService.GetUserId();
            var roles = _userService.GetRoles();
            if (!string.IsNullOrWhiteSpace(userId) && !roles.Contains(Constants.Roles.ROLE_ADMIN))
            {
                request.UserId = userId;
            }

            var claims = await _claimRepository.GetClaimsAsync(request);

            var message = claims == null || !claims.Any() ? "No claims found matching the criteria" : "Claims retrieved successfully";

            var claimResponses = _mapper.Map<IEnumerable<ClaimResponse>>(claims);

            return ApiResponse<IEnumerable<ClaimResponse>>.BuildResponse(
                StatusCodes.Status200OK,
                message,
                claimResponses
            );
        }

        public async Task<ApiResponse<ClaimResponse>> CreateClaimAsync(UpSertClaimRequest request)
        {
            var claim = new ClaimModel
            {
                CustomerName = request.CustomerName,
                Amount = request.Amount,
                Description = request.Description,
                Status = ClaimStatus.Pending,
                SubmitDate = DateTime.Now,
                UserId = _userService.GetUserId()
            };

            claim = await _claimRepository.CreateClaimAsync(claim);

            var claimResponse = _mapper.Map<ClaimResponse>(claim);

            return ApiResponse<ClaimResponse>.BuildResponse(
                StatusCodes.Status201Created,
                "Create claim successfully",
                claimResponse
            );
        }

        public async Task<ApiResponse<ClaimResponse>> GetClaimByIdAsync(string id)
        {
            var claim = await GetClaimOrError(id);

            if (claim == null)
            {
                return ApiResponse<ClaimResponse>.BuildErrorResponse(
                    StatusCodes.Status400BadRequest,
                    "Claim not found"
                );
            }

            var claimResponse = _mapper.Map<ClaimResponse>(claim);

            return ApiResponse<ClaimResponse>.BuildResponse(
                StatusCodes.Status200OK,
                "Get claim by id successfully",
                claimResponse
            );
        }

        public async Task<ApiResponse<ClaimResponse>> UpdateClaimAsync(string id, UpSertClaimRequest request)
        {
            var claim = await GetClaimOrError(id);

            if (claim == null)
            {
                return ApiResponse<ClaimResponse>
                        .BuildErrorResponse(StatusCodes.Status404NotFound, "Claim not found");
            }

            if (claim.Status != ClaimStatus.Pending)
            {
                return ApiResponse<ClaimResponse>
                        .BuildErrorResponse(StatusCodes.Status400BadRequest, "Can not update. Claim is already processed");
            }

            claim.CustomerName = request.CustomerName;
            claim.Amount = request.Amount;
            claim.Description = request.Description;

            claim = await _claimRepository.UpdateClaimAsync(claim);

            var claimResponse = _mapper.Map<ClaimResponse>(claim);

            return ApiResponse<ClaimResponse>.BuildResponse(
                StatusCodes.Status200OK,
                "Update claim successfully",
                claimResponse
            );
        }

        public async Task<ApiResponse<ClaimResponse>> DeleteClaimAsync(string id)
        {
            var claim = await GetClaimOrError(id);

            if (claim == null)
            {
                return ApiResponse<ClaimResponse>
                        .BuildErrorResponse(StatusCodes.Status404NotFound, "Claim not found");
            }

            if (claim.Status != ClaimStatus.Pending)
            {
                return ApiResponse<ClaimResponse>
                        .BuildErrorResponse(StatusCodes.Status400BadRequest, "Can not delete. Claim is already processed");
            }

            await _claimRepository.DeleteClaimAsync(claim);

            var claimResponse = _mapper.Map<ClaimResponse>(claim);

            return ApiResponse<ClaimResponse>.BuildResponse(
                StatusCodes.Status200OK,
                "Delete claim successfully",
                claimResponse
            );
        }

        public async Task<ApiResponse<ClaimResponse>> ProcessClaimAsync(string id)
        {
            var claim = await GetClaimOrError(id);

            if (claim == null)
            {
                return ApiResponse<ClaimResponse>
                        .BuildErrorResponse(StatusCodes.Status404NotFound, "Claim not found");
            }

            var random = new Random();

            claim.Status = (ClaimStatus)random.Next(
                                                    (int)ClaimStatus.Approved,
                                                    (int)ClaimStatus.Rejected + 1
                                               );

            claim = await _claimRepository.UpdateClaimAsync(claim);

            var claimResponse = _mapper.Map<ClaimResponse>(claim);

            return ApiResponse<ClaimResponse>.BuildResponse(
                StatusCodes.Status200OK,
                "Process claim successfully",
                claimResponse
            );
        }

        // Helper Methods
        private async Task<ClaimModel> GetClaimOrError(string id)
        {
            // User can only get their claims, admin can get all claims of users
            var userId = _userService.GetUserId();
            var roles = _userService.GetRoles();
            if (!roles.Contains(Constants.Roles.ROLE_ADMIN) && !await IsClaimBelongsToUser(userId, id))
            {
                return null;
            }

            return await _claimRepository.GetClaimByIdAsync(Guid.Parse(id));
        }

        private async Task<bool> IsClaimBelongsToUser(string userId, string claimId)
        {
            var claim = await _claimRepository.IsClaimBelongsToUser(userId, claimId);

            return claim != null ? true : false;
        }
    }
}
