using InsuranceClaimSystem.DTOs.Claim.Request;
using InsuranceClaimSystem.Models;

namespace InsuranceClaimSystem.Repositories
{
    public interface IClaimRepository
    {
        Task<IEnumerable<Claim>> GetClaimsAsync(GetClaimRequest request);
        Task<Claim> CreateClaimAsync(Claim claim);
        Task<Claim> GetClaimByIdAsync(Guid claimId);
        Task<Claim> UpdateClaimAsync(Claim claim);
        Task DeleteClaimAsync(Claim claim);
        Task<Claim> IsClaimBelongsToUser(string userId, string claimId);
    }
}
