namespace Programming_Contest_Platform.Helper;

public static class AppPolicies
{
    public const string AdminOnly = "Admin-only";
    public const string UserOnly = "User-only";
}

public static class AppCustomClaims
{
    public const string UserId = "uid";
    public const string Permissions = "permissions";
}