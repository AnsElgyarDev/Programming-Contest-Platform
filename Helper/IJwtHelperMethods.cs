using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Helper;

public interface IJwtHelperService
{
    public Task<TokenResponseDto> CreateTokenResponseAsync(User user);
    public Task<string>CreateAccessToken(User user);
    public Task<string> GenerateAndSaveRefreshTokenAsync(User user);
}