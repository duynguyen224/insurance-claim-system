using AutoMapper;
using InsuranceClaimSystem.DTOs;
using InsuranceClaimSystem.DTOs.User;
using InsuranceClaimSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace InsuranceClaimSystem.Services.User
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }

        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IEnumerable<string> GetRoles()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value) ?? Enumerable.Empty<string>();
        }

        public async Task<ApiResponse<IEnumerable<UserResponse>>> GetUsersAsync()
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(Constants.Roles.ROLE_USER);

            var users = _mapper.Map<IEnumerable<UserResponse>>(usersInRole);

            return ApiResponse<IEnumerable<UserResponse>>.BuildResponse(
                StatusCodes.Status200OK,
                "Get users successfully",
                users
            );
        }
    }
}
