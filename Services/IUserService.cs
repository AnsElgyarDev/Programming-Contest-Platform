using  Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
namespace Programming_Contest_Platform.Services;

public interface IUserService
{
    public Task<ServiceResult<string>> DeleteUserAsync(Guid userId);
    public Task<ServiceResult<string>> UpdateUserAsync(Guid userId, UpdateUserDto updateUserDto);
}