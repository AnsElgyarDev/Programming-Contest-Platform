using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.Endpoints;
using Programming_Contest_Platform.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); 
}


await app.UseUserEndpoints();
await app.UseContestEndpoints();
app.Run();