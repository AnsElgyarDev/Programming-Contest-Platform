using  Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
namespace Programming_Contest_Platform.Services;

public interface IUserService
{
    public Task<ServiceResult<string>> RegisterUserAsync(RegisterUserDto registerUserDto);
    public Task<ServiceResult<string>> SignInUserAsync(SignInUserDto signInUserDto);
    
    /* showing User Profile and user update to the acc and also delete the account. */
}