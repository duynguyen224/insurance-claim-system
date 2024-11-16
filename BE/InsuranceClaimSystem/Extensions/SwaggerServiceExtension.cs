using Microsoft.OpenApi.Models;

namespace InsuranceClaimSystem.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "INSURANCE CLAIM SYSTEM",
                    Version = "v1",
                    Description = "Insurance claim system APIs",
                    Contact = new OpenApiContact
                    {
                        Name = "Nguyen Duc Duy - Github - ",
                        Url = new Uri("https://github.com/duynguyen224")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                    },
                });

                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JSON Web Token to access resources. Example: Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new [] { string.Empty }
                    }
                });
            });

            return services;
        }
    }
}
