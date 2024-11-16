using InsuranceClaimSystem.DTOs;
using InsuranceClaimSystem.DTOs.Auth;

namespace InsuranceClaimSystem.Services.Auth
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> LoginAsync(LoginRequest request);
    }
}
