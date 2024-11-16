using InsuranceClaimSystem.DTOs;
using InsuranceClaimSystem.DTOs.Claim.Request;
using InsuranceClaimSystem.DTOs.Claim.Response;
using InsuranceClaimSystem.Models;

namespace InsuranceClaimSystem.Services.Claim
{
    public interface IClaimService
    {
        Task<ApiResponse<ClaimResponse>> CreateClaimAsync(UpSertClaimRequest request);
        Task<ApiResponse<ClaimResponse>> GetClaimByIdAsync(string id);
        Task<ApiResponse<IEnumerable<ClaimResponse>>> GetClaimsByStatusAsync(ClaimStatus status);
        Task<ApiResponse<ClaimResponse>> UpdateClaimAsync(string id, UpSertClaimRequest request);
        Task<ApiResponse<bool>> DeleteClaimAsync(string id);
        Task<ApiResponse<bool>> ProcessClaimAsync(string id);
    }
}
