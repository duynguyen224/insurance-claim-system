using InsuranceClaimSystem.Models;

namespace InsuranceClaimSystem.Repositories
{
    public interface IClaimRepository
    {
        Task<Claim> CreateClaimAsync(Claim claim);
        Task<Claim> GetClaimByIdAsync(Guid claimId);
        Task<IEnumerable<Claim>> GetClaimsByStatusAsync(ClaimStatus status);
        Task UpdateClaimAsync(Claim claim);
        Task DeleteClaimAsync(Claim claim);
    }
}
