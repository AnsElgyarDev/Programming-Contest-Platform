using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Services;

public interface IAuthService
{
    public Task<User?> RegisterUserAsync(UserDto registerUserDto);
    public Task<ServiceResult<string>> SignInUserAsync(UserDto signInUserDto);
    Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
}