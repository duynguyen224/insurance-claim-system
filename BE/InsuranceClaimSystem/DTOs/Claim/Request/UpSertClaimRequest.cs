using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceClaimSystem.DTOs.Claim.Request
{
    public class UpSertClaimRequest
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
