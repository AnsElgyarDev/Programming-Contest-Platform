using Programming_Contest_Platform.Services;
using Scalar.AspNetCore;

public class SessionManager : ISessionManager
{
    public string GetUserTheme()
    {
        // return HttpContext.Request.Cookies[ThemeCookieKey] ?? "Light"; 
        return "";
    }

    public void SetUserTheme(string theme)
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(30), 
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };

        // HttpContext.Response.Cookies.Append(ThemeMode.Dark, theme, cookieOptions);
    }
}