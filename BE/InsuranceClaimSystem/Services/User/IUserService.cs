using InsuranceClaimSystem.DTOs;
using InsuranceClaimSystem.DTOs.User;

namespace InsuranceClaimSystem.Services.User
{
    public interface IUserService
    {
        string GetUserId();
        IEnumerable<string> GetRoles();
        Task<ApiResponse<IEnumerable<UserResponse>>> GetUsersAsync();
    }
}
