namespace InsuranceClaimSystem.DTOs.Auth
{
    /// <summary>
    /// The login response
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// The user information
        /// </summary>
        /// <example></example>
        public UserResponse User;

        /// <summary>
        /// The access token (Jwt bearer token)
        /// </summary>
        /// <example></example>
        public string Token;
    }

    /// <summary>
    /// The user information
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// The user's full name
        /// </summary>
        /// <example>Duy Nguyen Duc</example>
        public string FullName { get; set; }

        /// <summary>
        /// The user name
        /// </summary>
        /// <example>user</example>
        public string UserName { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        /// <example>user01@gmail.com</example>
        public string Email { get; set; }

        /// <summary>
        /// The list roles of user
        /// </summary>
        /// <example>["user"]</example>
        public string[] Roles { get; set; }
    }
}
