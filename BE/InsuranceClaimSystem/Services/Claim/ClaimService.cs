using AutoMapper;
using InsuranceClaimSystem.DTOs;
using InsuranceClaimSystem.DTOs.Claim.Request;
using InsuranceClaimSystem.DTOs.Claim.Response;
using InsuranceClaimSystem.Models;
using InsuranceClaimSystem.Repositories;
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
            return claim == null ? BuildErrorResponse("Claim not found", StatusCodes.Status400BadRequest) : BuildApiResponse(claim, "Get claim by id successfully", StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<IEnumerable<ClaimResponse>>> GetClaimsByStatusAsync(ClaimStatus status)
        {
            var claims = await _claimRepository.GetClaimsByStatusAsync(status);

            if (claims == null || !claims.Any())
            {
                return ApiResponse<IEnumerable<ClaimResponse>>.BuildErrorResponse(
                    StatusCodes.Status404NotFound,
                    "No claims found for the given status"
                );
            }

            var claimResponses = _mapper.Map<IEnumerable<ClaimResponse>>(claims);

            return ApiResponse<IEnumerable<ClaimResponse>>.BuildResponse(
                StatusCodes.Status200OK,
                "Claims retrieved successfully",
                claimResponses
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
            return await _claimRepository.GetClaimByIdAsync(Guid.Parse(id));
        }
    }
}
