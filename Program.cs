using System.Text;
using DoctorsManagementSystem.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.Endpoints;
using Programming_Contest_Platform.Middleware;
using Programming_Contest_Platform.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContestService, ContestService>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!))
    };
});

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(); 
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseMiddleware<RequestLogMiddleware>();



await app.UseUserEndpoints();
await app.UseContestEndpoints();
await app.UseSubmissionEndpoints();
app.Run();