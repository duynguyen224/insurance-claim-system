namespace InsuranceClaimSystem.DTOs.Claim.Request
{
    /// <summary>
    /// The object request to filter claims
    /// </summary>
    public class GetClaimRequest
    {
        /// <summary>
        /// The uuid of user that create the claim
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// The status of claim (pending, approved, rejected)
        /// </summary>
        /// <example>pending</example>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// The submit date of claim (yyyy-mm-dd)
        /// </summary>
        /// <example>2024-02-28</example>
        public string SubmitDate { get; set; } = string.Empty;
    }
}
