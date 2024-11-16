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

                    var response = ApiResponse<object>
                                        .BuildErrorResponse(
                                            statusCode: StatusCodes.Status400BadRequest,
                                            message: "Invalid request data",
                                            errors: errors
                                        );

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
