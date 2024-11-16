using InsuranceClaimSystem.DTOs.Auth;

namespace InsuranceClaimSystem.Services.Auth
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequest request);
    }
}
