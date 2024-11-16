using InsuranceClaimSystem.DTOs;
using InsuranceClaimSystem.DTOs.Claim.Request;
using InsuranceClaimSystem.DTOs.Claim.Response;
using InsuranceClaimSystem.Models;
using InsuranceClaimSystem.Repositories;

namespace InsuranceClaimSystem.Services.Claim
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimService(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<ApiResponse<ClaimResponse>> CreateClaimAsync(UpSertClaimRequest request)
        {
            var res = new ApiResponse<ClaimResponse>.Builder();

            return res.Build();
        }

        public async Task<ApiResponse<ClaimResponse>> GetClaimByIdAsync(string id)
        {
            var res = new ApiResponse<ClaimResponse>.Builder();

            return res.Build();
        }

        public async Task<ApiResponse<IEnumerable<ClaimResponse>>> GetClaimsByStatusAsync(ClaimStatus status)
        {
            var res = new ApiResponse<IEnumerable<ClaimResponse>>.Builder();

            return res.Build();
        }

        public async Task<ApiResponse<ClaimResponse>> UpdateClaimAsync(string id, UpSertClaimRequest request)
        {
            var res = new ApiResponse<ClaimResponse>.Builder();

            return res.Build();
        }

        public async Task<ApiResponse<bool>> DeleteClaimAsync(string id)
        {
            var res = new ApiResponse<bool>.Builder();

            return res.Build();
        }

        public async Task<ApiResponse<bool>> ProcessClaimAsync(string id)
        {
            var res = new ApiResponse<bool>.Builder();

            return res.Build();
        }
    }
}
