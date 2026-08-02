namespace Programming_Contest_Platform.Helper;

public static class SupportedLanguages
{
    public static readonly List<string> All = new()
    {
        "cpp",
        "csharp",
        "python",
        "Go",
        "JS",
        "TS",
        "java",
        "kotlin"
    };

    public static bool IsSupported(string language)
    {
        return All.Contains(language.ToLower());
    }
}