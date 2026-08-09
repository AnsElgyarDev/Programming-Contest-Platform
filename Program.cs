using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.Endpoints;
using Programming_Contest_Platform.Helper;
using Programming_Contest_Platform.Middleware;
using Programming_Contest_Platform.Middlewares;
using Programming_Contest_Platform.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtHelperService, JwtHelperService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContestService, ContestService>();
builder.Services.AddScoped<ISessionManager, SessionManager>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddAppPolicies();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);  
    options.Cookie.Name = ".ECommerceApp.Session";
    options.Cookie.HttpOnly = true;                  
    options.Cookie.IsEssential = true;                
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
    options.Cookie.SameSite = SameSiteMode.Strict;    
});
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
        ClockSkew = TimeSpan.Zero, // to remove the clock-skew default time which is about 5 mins  
        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

var app = builder.Build();

app.UseExceptionHandler();
app.UseRouting();

app.UseSession();

app.MapGet("/", () => Results.Redirect("/scalar/v1"))
   .ExcludeFromDescription();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); 
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseMiddleware<RequestLogMiddleware>();


await app.UseAuthEndpoints();
await app.MapUserEndpoints();
await app.UseContestEndpoints();
await app.UseSubmissionEndpoints();
app.Run();