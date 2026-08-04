using System.Security.Claims;
namespace Programming_Contest_Platform.Helper.ClaimsPrincipalExtensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var claimValue = user.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid.TryParse(claimValue, out var userId);
        return userId;
    }
}