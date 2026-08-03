using Microsoft.AspNetCore.Identity;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Helper;
using FluentValidation;

namespace Programming_Contest_Platform.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public UserService(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        this._userManager = userManager;
        this._jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ServiceResult<string>> DeleteUserAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return ServiceResult<string>.Failure("There is no User With This ID!");
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Failed to delete the user.";
            return ServiceResult<string>.Failure(errorMessage);
        }

        return ServiceResult<string>.Success("The Operation Completed Successfully!");
    }

    public async Task<ServiceResult<string>> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if(user is null)
        {
            return ServiceResult<string>.Failure("There is No User With This ID !");
        }

        user.FullName = updateUserDto.FullName ?? user.FullName;
        user.Country = updateUserDto.Country ?? user.Country;
        user.Organization = updateUserDto.Organization ?? user.Organization;
        user.ProfilePictureUrl = updateUserDto.ProfilePictureUrl ?? user.ProfilePictureUrl;
             
        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
        {
            var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Failed to Update the user.";
            return ServiceResult<string>.Failure(errorMessage);
        }

        return ServiceResult<string>.Success("Updated User Successfully!");
    }
}