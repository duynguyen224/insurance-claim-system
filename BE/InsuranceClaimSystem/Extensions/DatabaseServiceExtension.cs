using InsuranceClaimSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace InsuranceClaimSystem.Extensions
{
    public static class DatabaseServiceExtension
    {
        public static IServiceCollection AddDatabaseService(this IServiceCollection services)
        {
            // Add DbContext with In-Memory database
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("InsuranceClaimInMemoryDb"));

            return services;
        }
    }
}
