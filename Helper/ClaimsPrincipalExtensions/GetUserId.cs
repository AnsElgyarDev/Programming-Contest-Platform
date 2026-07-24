using System.Security.Claims;
namespace Programming_Contest_Platform.Helper.ClaimsPrincipalExtensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var claimValue = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(claimValue, out var userId) ? userId : 0;
    }
}