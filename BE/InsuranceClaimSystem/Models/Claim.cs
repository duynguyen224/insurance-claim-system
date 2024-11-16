using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaimSystem.Models
{
    public class Claim
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.0, int.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime SubmitDate { get; set; }

        [Required]
        public ClaimStatus Status { get; set; }

        public string? UserId { get; set; }
    }
}
