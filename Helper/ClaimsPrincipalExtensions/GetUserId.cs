using System.Security.Claims;
namespace Programming_Contest_Platform.Helper.ClaimsPrincipalExtensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                        ?? user.FindFirst("sub")?.Value;

        return Guid.TryParse(userIdStr, out var userId) ? userId : Guid.Empty;
    }
}