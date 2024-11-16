using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceClaimSystem.DTOs.Claim.Request
{
    public class UpSertClaimRequest
    {
        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.0, int.MaxValue, ErrorMessage = "Amount must be a positive value and cannot exceed 2147483647")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
