public class SessionManager
{
    public string GetUserTheme()
    {
        return HttpContext.Request.Cookies[ThemeCookieKey] ?? "Light"; 
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

        HttpContext.Response.Cookies.Append(ThemeCookieKey, theme, cookieOptions);
    }
}