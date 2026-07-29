using DoctorsManagementSystem.Middlewares;
using Microsoft.AspNetCore.Identity;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.Endpoints;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Helper;
using Programming_Contest_Platform.Middleware;
using Programming_Contest_Platform.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContestService, ContestService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false; 
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestLogMiddleware>();

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); 
}


await app.UseUserEndpoints();
await app.UseContestEndpoints();
await app.UseSubmissionEndpoints();
app.Run();