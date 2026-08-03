using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Services;

public interface IAuthService
{
    public Task<ServiceResult<string>> RegisterUserAsync(UserDto registerUserDto);
    public Task<ServiceResult<string>> SignInUserAsync(UserDto signInUserDto);
}