using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Helper;

namespace Programming_Contest_Platform.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    
    public AuthService(AppDbContext context)
    {
        _context  = context;
    }

    public Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        throw new NotImplementedException();
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

    public async Task<ServiceResult<string>> SignInUserAsync(UserDto signInDto)
    {
        // var user = await _userManager!.FindByEmailAsync(signInDto.UserEmail);        
        
        // if(user is null)
        // {
        //     return ServiceResult<string>.Failure("There is Something Wrong in Email or Password");
        // }

        // var isPasswordValid = await _userManager.CheckPasswordAsync(user, signInDto.UserPassword);
        
        // if(!isPasswordValid)
        // {
        //     return ServiceResult<String>.Failure("Invalid Email or Password!");   
        // }

        // var token = _jwtTokenGenerator.GenerateToken(user);

        // return ServiceResult<string>.Success(token);

    }
}