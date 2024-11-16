using InsuranceClaimSystem.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaimSystem.Extensions
{
    public static class ApiBehaviourOptionExtension
    {
        public static IServiceCollection AddApiBehaviorOptions(this IServiceCollection services)
        {
            services.AddMvc()
                    .ConfigureApiBehaviorOptions(options =>
                        {
                            options.SuppressModelStateInvalidFilter = false;
                        });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(entry => entry.Value.Errors.Any())
                        .ToDictionary(
                            entry => entry.Key,
                            entry => entry.Value.Errors.Select(e => e.ErrorMessage).ToList()
                        );

                    var response = new ApiResponse<object>.Builder()
                    .SetStatusCode(StatusCodes.Status400BadRequest)
                    .SetMessage("Invalid request data")
                    .SetErrors(errors)
                    .Build();

                    return new BadRequestObjectResult(response)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                };
            });

            return services;
        }
    }
}
