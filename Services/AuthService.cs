// using Programming_Contest_Platform.DTO;
// using Programming_Contest_Platform.Entity;

// namespace Programming_Contest_Platform.Services;

// public class AuthService : IAuthService
// {
//     public async Task<ServiceResult<string>> RegisterUserAsync(RegisterUserDto registerUserDto)
//     {

//         var existingEmail = await _userManager!.FindByEmailAsync(registerUserDto.UserEmail);
        
//         if(existingEmail is not null)
//         {
//             return ServiceResult<string>.Failure("There is already a user with this email.");
//         }

//         var user = new User
//         {
//             UserName = registerUserDto.UserName,
//             Email = registerUserDto.UserEmail,
//         };

//         var result = await _userManager.CreateAsync(user, registerUserDto.UserPassword);

//         if (!result.Succeeded)
//         {
//             var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Registration failed.";
//             return ServiceResult<string>.Failure(errorMessage);
//         }

//         var token = _jwtTokenGenerator.GenerateToken(user);

//         return ServiceResult<string>.Success(token);
//     }

//     public async Task<ServiceResult<string>> SignInUserAsync(SignInUserDto signInDto)
//     {
//         var user = await _userManager!.FindByEmailAsync(signInDto.UserEmail);        
        
//         if(user is null)
//         {
//             return ServiceResult<string>.Failure("There is Something Wrong in Email or Password");
//         }

//         var isPasswordValid = await _userManager.CheckPasswordAsync(user, signInDto.UserPassword);
        
//         if(!isPasswordValid)
//         {
//             return ServiceResult<String>.Failure("Invalid Email or Password!");   
//         }

//         var token = _jwtTokenGenerator.GenerateToken(user);

//         return ServiceResult<string>.Success(token);

//     }
// }