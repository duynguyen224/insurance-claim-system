using InsuranceClaimSystem.Data;
using InsuranceClaimSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly AppDbContext _context;

        public ClaimRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Claim> CreateClaimAsync(Claim claim)
        {
            await _context.Claims.AddAsync(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task<Claim> GetClaimByIdAsync(Guid claimId)
        {
            return await _context.Claims.FindAsync(claimId);
        }

        public async Task<IEnumerable<Claim>> GetClaimsByStatusAsync(ClaimStatus status)
        {
            return await _context.Claims.Where(x => x.Status == status).ToListAsync();
        }

        public async Task<Claim> UpdateClaimAsync(Claim claim)
        {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task DeleteClaimAsync(Claim claim)
        {
            _context.Claims.Remove(claim);
            await _context.SaveChangesAsync();
        }
    }
}
