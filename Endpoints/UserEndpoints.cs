using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Helper.ClaimsPrincipalExtensions;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class UseUserEndpoints
{
    public static async Task MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/users")
                       .WithTags("Users Management")
                       .RequireAuthorization(); 

        // DELETE /api/users/me
        group.MapDelete("/me", async Task<Results<NotFound<string>, Ok>> 
            (ClaimsPrincipal userContext, IUserService userService) =>
        {
            Guid userId = userContext.GetUserId();

            var isSuccess = await userService.DeleteUserAsync(userId);

            if (isSuccess.isFailure)
            {
                return TypedResults.NotFound(isSuccess.ErrorMessage);
            }

            return TypedResults.Ok();
        });

        // PUT /api/users/me
        group.MapPut("/me", async Task<Results<NotFound<string>, BadRequest<string>, Ok<string>>> 
            (ClaimsPrincipal userContext, UpdateUserDto dto, IUserService userService) =>
        {
            Guid userId = userContext.GetUserId();

            var result = await userService.UpdateUserAsync(userId, dto);

            if (result.isFailure)
            {
                return TypedResults.NotFound(result.ErrorMessage);
            }

            return TypedResults.Ok(result.Data);
        });
    }
}