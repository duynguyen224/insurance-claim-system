using InsuranceClaimSystem.Data;
using InsuranceClaimSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace InsuranceClaimSystem.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}
