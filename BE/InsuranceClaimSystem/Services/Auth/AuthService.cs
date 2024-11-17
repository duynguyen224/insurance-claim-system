using InsuranceClaimSystem.DTOs;
using InsuranceClaimSystem.DTOs.Auth;
using InsuranceClaimSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SecurityClaim = System.Security.Claims.Claim;

namespace InsuranceClaimSystem.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse<object>> LoginAsync(LoginRequest request)
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return ApiResponse<object>.BuildErrorResponse(
                    StatusCodes.Status401Unauthorized,
                    "Invalid credentials"
                );
            }

            // Verify the password
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return ApiResponse<object>.BuildErrorResponse(
                    StatusCodes.Status401Unauthorized,
                    "Invalid credentials"
                );
            }

            // Generate JWT token
            string token = await GenerateJwtToken(user);

            var dataResponse = new 
            {
                User = new
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                },
                Token = token
            };

            // Return success response with user data and token
            return ApiResponse<object>.BuildResponse(
                StatusCodes.Status200OK,
                "Login successfully",
                dataResponse
            );
        }

        private async Task<string> GenerateJwtToken(AppUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            // User claims
            var claims = new List<SecurityClaim>
            {
                new SecurityClaim(ClaimTypes.NameIdentifier, user.Id),
                new SecurityClaim(JwtRegisteredClaimNames.Sub, user.Email),
                new SecurityClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Add role claims
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new SecurityClaim(ClaimTypes.Role, role)));

            // Generate the JWT token
            var key = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
