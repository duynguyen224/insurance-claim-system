namespace InsuranceClaimSystem.DTOs.User
{
    /// <summary>
    /// The claim response
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// The id of user
        /// </summary>
        /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
        public string Id { get; set; }

        /// <summary>
        /// The user full name
        /// </summary>
        /// <example>Duy Nguyen Duc</example>
        public string FullName { get; set; }
    }
}
