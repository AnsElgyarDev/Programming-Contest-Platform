using BCrypt.Net;
namespace Programming_Contest_Platform.Helper;

public class PasswordHasher
{
    private const int WorkFactor = 12;
    public static string HashPassword(string PlainText) =>
        BCrypt.Net.BCrypt.HashPassword(PlainText, WorkFactor);
    
    public static bool VerifyPassword(string plainPassword, string hashedPassword) => 
        BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
}