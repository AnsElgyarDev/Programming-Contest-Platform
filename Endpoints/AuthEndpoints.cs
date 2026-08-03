using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Helper.ClaimsPrincipalExtensions;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class AuthEndpoints
{
    public static async Task UseAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/users").WithTags("Users Authentication");
        app.MapGet("/", () => Results.Redirect("/scalar/v1"));    
        // Post endpoints
        group.MapPost("signin", async Task<Results<BadRequest<string>, Ok<string>>>
            (IAuthService userService, UserDto signInUserDto) =>
        {
            var tokenResponseDto = await userService.SignInUserAsync(signInUserDto);

            if(tokenResponseDto is null)
            {
                return TypedResults.BadRequest("Something Wrong in UserName or Password!");
            }

            return TypedResults.Ok("User signed-in Successfully");
            // if (serviceResult.isFailure)
            // {
            //     return TypedResults.BadRequest(serviceResult.ErrorMessage);
            // }

            // return TypedResults.Ok(serviceResult.Data);
        });
        
        group.MapPost("register", async 
            (IAuthService userService, UserDto registerUserDto) =>
        {
            var serviceResult = await userService.RegisterUserAsync(registerUserDto);

            // if (serviceResult.isFailure)
            // {
            //     return TypedResults.BadRequest(serviceResult.ErrorMessage);
            // }

            // return TypedResults.Created("/api/users/signin", "Registered Successfully!");
        });
    }
}