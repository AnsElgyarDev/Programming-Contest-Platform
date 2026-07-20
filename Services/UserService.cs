using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Helper;

namespace Programming_Contest_Platform.Services;

public class UserService : IUserService
{
    private readonly AppDbContext? _context;
    public UserService(AppDbContext? context)
    {
        this._context = context;
    }
    public async Task<ServiceResult> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        if(await _context!.Users.AnyAsync(email => email.UserEmail == registerUserDto.UserEmail))
        {
            return ServiceResult.Failure("There is already User with This Email");
        }
        var hashedPassword = PasswordHasher.HashPassword(registerUserDto.UserPassword);
        
        var user = new User
        {
            UserName = registerUserDto.UserName,  
            UserEmail = registerUserDto.UserEmail,  
            UserPassword = hashedPassword
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return ServiceResult.Success();
    }

    public async Task<ServiceResult> SignInUserAsync(SignInUserDto signInDto)
    {
        var user = await _context!.Users.FirstOrDefaultAsync(u => u.UserEmail == signInDto.UserEmail);
        
        if(user is null)
        {
            return ServiceResult.Failure("There is Something Wrong in Email or Password");
        }

        var verifiyPassword =  PasswordHasher.VerifyPassword(signInDto.UserPassword, user.UserPassword);

        if(!verifiyPassword)
        {
            return ServiceResult.Failure("Password is Wrong!");   
        }

        return ServiceResult.Success();
    }
}