using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using FluentValidation;
using Programming_Contest_Platform.Data;
using Microsoft.EntityFrameworkCore;

namespace Programming_Contest_Platform.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IEncryptionService _encryptionService;

    public UserService(AppDbContext context, IEncryptionService encryptionService)
    {
        _context = context;
        _encryptionService = encryptionService;
    }

    public async Task<UserProfileDto?> GetUserProfileAsync(Guid? userId)
    {
        if (userId is null || userId == Guid.Empty)
        {
            return null;
        }
        
        return await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(user => new UserProfileDto
            {
                Username = _encryptionService.Decrypt(user.Username),
                FullName = user.FullName,
                Country = user.Country,
                Organization = user.Organization,
                ProfilePictureUrl = user.ProfilePictureUrl,
                UserRating = user.UserRating,
                MaxRating = user.MaxRating,
                SolvedProblemsCount = user.SolvedProblemsCount
            })
            .FirstOrDefaultAsync();
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