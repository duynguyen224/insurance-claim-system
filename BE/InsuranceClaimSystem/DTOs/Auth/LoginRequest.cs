using System.ComponentModel.DataAnnotations;

namespace InsuranceClaimSystem.DTOs.Auth
{
    /// <summary>
    /// The login request
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// The email of user
        /// </summary>
        /// <example>admin@gmail.com</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// The password of user
        /// </summary>
        /// <example>Admin@123</example>
        [Required]
        public string Password { get; set; }
    }
}
