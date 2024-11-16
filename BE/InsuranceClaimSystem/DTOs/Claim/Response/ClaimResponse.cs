using InsuranceClaimSystem.Models;

namespace InsuranceClaimSystem.DTOs.Claim.Response
{
    /// <summary>
    /// The claim response
    /// </summary>
    public class ClaimResponse
    {
        /// <summary>
        /// The id of claim
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public string Id { get; set; }

        /// <summary>
        /// The customer name of claim
        /// </summary>
        /// <example>Duy Nguyen Duc</example>
        public string CustomerName { get; set; }

        /// <summary>
        /// The amount of claim
        /// </summary>
        /// <example>500</example>
        public decimal Amount { get; set; }

        /// <summary>
        /// The description of claim
        /// </summary>
        /// <example>Claim description</example>
        public string Description { get; set; }

        /// <summary>
        /// The customer name of claim
        /// </summary>
        /// <example>2024-02-28</example>
        public DateTime SubmitDate { get; set; }

        /// <summary>
        /// The status of claim
        /// </summary>
        /// <example>0: pending, 1: approved, 2: rejected</example>
        public ClaimStatus Status { get; set; }
    }
}
