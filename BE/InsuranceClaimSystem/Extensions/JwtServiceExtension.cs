using InsuranceClaimSystem.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace InsuranceClaimSystem.Extensions
{
    public static class JwtServiceExtension
    {
        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                };

                o.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        // Kkip the default logic and avoid using the default response
                        context.HandleResponse();

                        // Handle 401 Unauthorized errors
                        var response = new ApiResponse<object>.Builder()
                            .SetStatusCode(StatusCodes.Status401Unauthorized)
                            .SetMessage("Unauthorized. Please login to access this resource.")
                            .SetIsSuccess(false)
                            .Build();

                        // Set the response content type and status code
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        // Return the custom response
                        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    },

                    OnForbidden = context =>
                    {
                        // Handle 403 Forbidden errors
                        var response = new ApiResponse<object>.Builder()
                            .SetStatusCode(StatusCodes.Status403Forbidden)
                            .SetMessage("Access denied. You don't have permission to perform this action.")
                            .Build();

                        // Set the response content type and status code
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;

                        // Return the custom response
                        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                };
            });

            return services;
        }
    }
}
