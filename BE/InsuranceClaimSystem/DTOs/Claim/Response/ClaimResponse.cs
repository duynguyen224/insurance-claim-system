using InsuranceClaimSystem.Models;

namespace InsuranceClaimSystem.DTOs.Claim.Response
{
    public class ClaimResponse
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime SubmitDate { get; set; }
        public ClaimStatus Status { get; set; }
    }
}
