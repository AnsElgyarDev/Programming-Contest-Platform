using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Helper;

namespace Programming_Contest_Platform.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IJwtHelperService _jwtHelperService;
    public AuthService(AppDbContext context, IJwtHelperService jwtHelperService)
    {
        _context  = context;
        _jwtHelperService = jwtHelperService;
    }

    public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var user = await _context.Users.FindAsync(request.UserId);
        if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return null;

        return await _jwtHelperService.CreateTokenResponseAsync(user);
    }

    public async Task<User?> RegisterUserAsync(UserDto registerUserDto)
    {
        var existingUser = await _context.Users.AnyAsync(user => user.Username == registerUserDto.UserName);
        
        if (existingUser)
        {
            return null;
        }

        var userToRegister = new User
        {
            Username = registerUserDto.UserName, 
            Role = string.IsNullOrEmpty(registerUserDto.Role) ? "User" : registerUserDto.Role
        };

        userToRegister.PasswordHash = new PasswordHasher<User>()
            .HashPassword(userToRegister, registerUserDto.UserPassword);

        _context.Users.Add(userToRegister);
        await _context.SaveChangesAsync();

        return userToRegister;
    }

    public async Task<TokenResponseDto> SignInUserAsync(UserDto signInDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == signInDto.UserName);

        if(user is null)
        {
            return null!;
        }
        
        var IsValidPassword = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, signInDto.UserPassword);

        if(IsValidPassword is PasswordVerificationResult.Failed)
        {
            return null!;
        }
        
        return await _jwtHelperService.CreateTokenResponseAsync(user);
    }
}