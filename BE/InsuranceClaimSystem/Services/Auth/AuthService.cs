﻿using InsuranceClaimSystem.DTOs.Auth;
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
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            // Find the user
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return null;

            // Verify the password
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) return null;

            // Generate the token
            return await GenerateJwtToken(user);
        }

        private async Task<string> GenerateJwtToken(AppUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            // User claims
            var claims = new List<SecurityClaim>
            {
                new SecurityClaim(JwtRegisteredClaimNames.Sub, user.UserName),
                new SecurityClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new SecurityClaim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Add role claims
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new SecurityClaim(ClaimTypes.Role, role)));

            // Generate the token
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