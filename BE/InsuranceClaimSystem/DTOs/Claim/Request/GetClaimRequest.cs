namespace InsuranceClaimSystem.DTOs.Claim.Request
{
    public class GetClaimRequest
    {
        public string Id { get; set; } = string.Empty;
        public string UserId {  get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string SubmitDate { get; set; } = string.Empty;
    }
}
