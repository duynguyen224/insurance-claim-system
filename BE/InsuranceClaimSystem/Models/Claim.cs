namespace InsuranceClaimSystem.Models
{
    public class Claim
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public decimal ClaimAmount { get; set; }
        public string ClaimDescription { get; set; }
        public DateTime ClaimDate { get; set; }
        public ClaimStatus Status { get; set; }
        public string CreatedByUserId { get; set; }
    }
}
