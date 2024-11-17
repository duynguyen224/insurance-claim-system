using InsuranceClaimSystem.DTOs.Claim;
using ClaimModel = InsuranceClaimSystem.Models.Claim;

namespace InsuranceClaimSystem.Repositories.Claim
{
    public interface IClaimRepository
    {
        Task<IEnumerable<ClaimModel>> GetClaimsAsync(GetClaimRequest request);
        Task<ClaimModel> CreateClaimAsync(ClaimModel claim);
        Task<ClaimModel> GetClaimByIdAsync(Guid claimId);
        Task<ClaimModel> UpdateClaimAsync(ClaimModel claim);
        Task DeleteClaimAsync(ClaimModel claim);
        Task<ClaimModel> IsClaimBelongsToUser(string userId, string claimId);
    }
}
