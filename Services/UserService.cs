using Microsoft.AspNetCore.Identity;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Helper;
using FluentValidation;
using Programming_Contest_Platform.Data;

namespace Programming_Contest_Platform.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<string>> DeleteUserAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user is null)
        {
            return ServiceResult<string>.Failure("There is no User With This ID!");
        }

        _context.Users.Remove(user);
        
        await _context.SaveChangesAsync();

        return ServiceResult<string>.Success("The Deletion Completed Successfully!");
    }

    public async Task<ServiceResult<string>> UpdateUserAsync(Guid userId, UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(userId);
        
        if(user is null)
        {
            return ServiceResult<string>.Failure("There is No User With This ID !");
        }

        user.FullName = updateUserDto.FullName ?? user.FullName;
        user.Country = updateUserDto.Country ?? user.Country;
        user.Organization = updateUserDto.Organization ?? user.Organization;
        user.ProfilePictureUrl = updateUserDto.ProfilePictureUrl ?? user.ProfilePictureUrl;
             
        await _context.SaveChangesAsync();

        return ServiceResult<string>.Success("Updated User Successfully!");
    }
}