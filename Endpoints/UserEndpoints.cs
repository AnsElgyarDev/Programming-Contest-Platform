using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class UserEndpoints
{
    public static async Task UseUserEndpoints(this WebApplication app)
    {
        // Post endpoints
        app.MapPost("api/users/signin", async Task<Results<BadRequest<string>, Ok<string>>>  
                  (IUserService userService,  SignInUserDto signInUserDto) =>
        {
            var serviceResult = await userService.SignInUserAsync(signInUserDto);
            
            if(serviceResult.isFailure)
            {
                return TypedResults.BadRequest(serviceResult.ErrorMessage);
            }

            return TypedResults.Ok("Signed in Successfully!");
        });  

        app.MapPost("api/users/register", async Task<Results<BadRequest<string>, Created<string>>>  
            (IUserService userService, RegisterUserDto registerUserDto) =>
        {
            var serviceResult = await userService.RegisterUserAsync(registerUserDto);
            
            if(serviceResult.isFailure)
            {
                return TypedResults.BadRequest(serviceResult.ErrorMessage);
            }

            return TypedResults.Created("/api/users/login", "Registered Successfully!");
        });  
    } 
}