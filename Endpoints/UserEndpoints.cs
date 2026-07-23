using Microsoft.AspNetCore.Http.HttpResults;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class UserEndpoints
{
    public static async Task UseUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/users").WithTags("Users Authentication");

        // Post endpoints
        group.MapPost("signin", async Task<Results<BadRequest<string>, Ok<string>>>
            (IUserService userService, SignInUserDto signInUserDto) =>
        {
            var serviceResult = await userService.SignInUserAsync(signInUserDto);

            if (serviceResult.isFailure)
            {
                return TypedResults.BadRequest(serviceResult.ErrorMessage);
            }

            return TypedResults.Ok(serviceResult.Data);
        });

        group.MapPost("register", async Task<Results<BadRequest<string>, Created<string>>>
            (IUserService userService, RegisterUserDto registerUserDto) =>
        {
            var serviceResult = await userService.RegisterUserAsync(registerUserDto);

            if (serviceResult.isFailure)
            {
                return TypedResults.BadRequest(serviceResult.ErrorMessage);
            }

            return TypedResults.Created("/api/users/signin", "Registered Successfully!");
        });

        app.MapGet("/", () => Results.Redirect("/scalar/v1"));
    }
}