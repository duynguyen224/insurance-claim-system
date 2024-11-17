using InsuranceClaimSystem.DTOs;
using InsuranceClaimSystem.DTOs.Claim;

namespace InsuranceClaimSystem.Services.Claim
{
    public interface IClaimService
    {
        Task<ApiResponse<IEnumerable<ClaimResponse>>> GetClaimsAsync(GetClaimRequest request);
        Task<ApiResponse<ClaimResponse>> CreateClaimAsync(UpSertClaimRequest request);
        Task<ApiResponse<ClaimResponse>> GetClaimByIdAsync(string id);
        Task<ApiResponse<ClaimResponse>> UpdateClaimAsync(string id, UpSertClaimRequest request);
        Task<ApiResponse<ClaimResponse>> DeleteClaimAsync(string id);
        Task<ApiResponse<ClaimResponse>> ProcessClaimAsync(string id);
    }
}
