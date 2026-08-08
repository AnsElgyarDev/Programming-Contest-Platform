namespace Programming_Contest_Platform.Services;

public interface ISessionManager
{
    // user Preferences 
    string GetUserTheme();
    void SetUserTheme(string theme);
} 