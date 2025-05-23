﻿using InsuranceClaimSystem.Data;
using InsuranceClaimSystem.DTOs.Claim;
using Microsoft.EntityFrameworkCore;
using ClaimModel = InsuranceClaimSystem.Models.Claim;

namespace InsuranceClaimSystem.Repositories.Claim
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly AppDbContext _context;

        public ClaimRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClaimModel>> GetClaimsAsync(GetClaimRequest request)
        {
            var query = _context.Claims.AsQueryable();

            // Filter by UserId if provided
            if (!string.IsNullOrEmpty(request.UserId))
            {
                query = query.Where(c => c.UserId == request.UserId);
            }

            // Filter by Status if provided
            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(c => c.Status.ToString().Equals(request.Status, StringComparison.OrdinalIgnoreCase));
            }

            // Filter by SubmitDate if provided
            if (!string.IsNullOrEmpty(request.SubmitDate))
            {
                if (DateTime.TryParse(request.SubmitDate, out DateTime submitDate))
                {
                    query = query.Where(c => c.SubmitDate.Date == submitDate.Date);
                }
            }

            return await query.OrderByDescending(x => x.SubmitDate).ToListAsync();
        }

        public async Task<ClaimModel> CreateClaimAsync(ClaimModel claim)
        {
            await _context.Claims.AddAsync(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task<ClaimModel> GetClaimByIdAsync(Guid claimId)
        {
            return await _context.Claims.FindAsync(claimId);
        }

        public async Task<ClaimModel> UpdateClaimAsync(ClaimModel claim)
        {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task DeleteClaimAsync(ClaimModel claim)
        {
            _context.Claims.Remove(claim);
            await _context.SaveChangesAsync();
        }

        public async Task<ClaimModel> IsClaimBelongsToUser(string userId, string claimId)
        {
            var claim = await _context.Claims
                                .Where(x => !string.IsNullOrWhiteSpace(x.UserId)
                                            && x.UserId.Equals(userId)
                                            && x.Id.ToString().Equals(claimId))
                                .FirstOrDefaultAsync();

            return claim;
        }
    }
}
