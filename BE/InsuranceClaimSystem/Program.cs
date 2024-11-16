using InsuranceClaimSystem.Data;
using InsuranceClaimSystem.Extensions;
using InsuranceClaimSystem.Mappings;
using InsuranceClaimSystem.Middlewares;
using InsuranceClaimSystem.Models;
using InsuranceClaimSystem.Repositories;
using InsuranceClaimSystem.Services.Auth;
using InsuranceClaimSystem.Services.Claim;
using InsuranceClaimSystem.Services.User;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// =============================
// Add services to the container
// =============================

// Add in-memory database service
builder.Services.AddDatabaseService();

// Add identity service
builder.Services.AddIdentityService();

// Add swagger
builder.Services.AddSwaggerService();

// Add jwt service
builder.Services.AddJwtService(builder.Configuration);

// Add Auto mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add api behavior options
builder.Services.AddApiBehaviorOptions();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Dependency injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
builder.Services.AddScoped<IClaimService, ClaimService>();

builder.Services.AddControllers()
                .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// ===============
// Middleware
// ===============
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// ===============
// Database Seeder
// ===============

// Seed roles and users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DataSeeder.SeedAsync(userManager, roleManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ===============
// Allow cors
// ===============
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
