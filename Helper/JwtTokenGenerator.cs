using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Helper;

public class JwtHelperService : IJwtHelperService
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    
    public JwtHelperService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<TokenResponseDto> CreateTokenResponseAsync(User user)
    {
        return new TokenResponseDto
        {
            AccessToken = CreateAccessToken(user),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
        };
    }

    public string CreateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!));
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1), // Expiry Time
            Issuer = _configuration["AppSettings:Issuer"],
            Audience = _configuration["AppSettings:Audience"],
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        var refreshToken = Convert.ToBase64String(randomNumber);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync();
        return refreshToken;
    }
}
