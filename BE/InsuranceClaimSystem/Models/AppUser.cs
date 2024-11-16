using Microsoft.AspNetCore.Identity;

namespace InsuranceClaimSystem.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
