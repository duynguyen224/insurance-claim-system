using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceClaimSystem.DTOs.Claim
{
    /// <summary>
    /// The object request to create a new claim
    /// </summary>
    public class UpSertClaimRequest
    {
        /// <summary>
        /// The customer name of claim
        /// </summary>
        /// <example>Duy Nguyen Duc</example>
        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; }

        /// <summary>
        /// The amount of claim
        /// </summary>
        /// <example>600</example>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.0, int.MaxValue, ErrorMessage = "Amount must be a positive value and cannot exceed 2147483647")]
        public decimal Amount { get; set; }

        /// <summary>
        /// The description of claim
        /// </summary>
        /// <example>Your claim description</example>
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
