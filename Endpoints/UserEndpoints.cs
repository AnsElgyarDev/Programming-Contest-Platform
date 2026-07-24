using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Helper.ClaimsPrincipalExtensions;
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

        app.MapDelete("/api/users/me", async Task<Results<NotFound<string>, Ok>> 
                    (ClaimsPrincipal user, IUserService userService) =>
        {
            int userId = user.GetUserId();

            var isSuccess = await userService.DeleteUserAsync(userId);

            if(isSuccess.isFailure)
            {
                return TypedResults.NotFound(isSuccess.ErrorMessage);
            }

            return  TypedResults.Ok();

        }).RequireAuthorization();

        app.MapPut("/api/users/me", async Task<Results<NotFound<string>, BadRequest<string>, Ok<string>>> 
                  (ClaimsPrincipal userContext, UpdateUserDto dto, IUserService userService) =>
    {
        int userId = userContext.GetUserId();

        var result = await userService.UpdateUserAsync(userId, dto);

        if (result.isFailure)
        {
            return TypedResults.NotFound(result.ErrorMessage);
        }

        return TypedResults.Ok(result.Data);
    }).RequireAuthorization();
    }
}