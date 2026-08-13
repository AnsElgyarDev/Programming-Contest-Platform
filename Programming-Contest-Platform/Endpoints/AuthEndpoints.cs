using Microsoft.AspNetCore.Http.HttpResults;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class AuthEndpoints
{
    public static async Task UseAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/users").WithTags("Users Authentication");
        
        group.MapPost("/register", async Task<Results<BadRequest<string>, Created<User?>>>
            (IAuthService userService, UserDto registerUserDto) =>
        {
            var user = await userService.RegisterUserAsync(registerUserDto);
            
            if(user is null)
            {
                return TypedResults.BadRequest("Username is already taken.");
            }
            
            return TypedResults.Created<User?>("/api/users/signin", user);

        });

        group.MapPost("/signin", async Task<Results<UnauthorizedHttpResult, Ok<TokenResponseDto>>>
            (IAuthService userService, UserDto signInUserDto) =>
        {
            var tokenResponseDto = await userService.SignInUserAsync(signInUserDto);

            if(tokenResponseDto is null)
            {
                return TypedResults.Unauthorized();
            }

            return TypedResults.Ok(tokenResponseDto);
            
        });

        group.MapPost("/refresh-token", async Task<Results<UnauthorizedHttpResult, Ok<TokenResponseDto>>>
            (IAuthService authService, RefreshTokenRequestDto refreshDto) =>
        {
            var tokenResponseDto = await authService.RefreshTokenAsync(refreshDto);

            if (tokenResponseDto is null)
            {
                return TypedResults.Unauthorized();
            }

            return TypedResults.Ok(tokenResponseDto);
        });
        
    }
}