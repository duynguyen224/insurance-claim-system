using InsuranceClaimSystem.Data;
using InsuranceClaimSystem.Extensions;
using InsuranceClaimSystem.Mappings;
using InsuranceClaimSystem.Models;
using InsuranceClaimSystem.Repositories;
using InsuranceClaimSystem.Services.Auth;
using InsuranceClaimSystem.Services.Claim;
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

// Adding Auto mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Dependency injection
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
builder.Services.AddScoped<IClaimService, ClaimService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// =============================
// Seeder
// =============================

// Seed roles and users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await DataSeeder.SeedAsync(userManager, roleManager);
}

app.Run();
