using Microsoft.AspNetCore.Identity;
namespace Programming_Contest_Platform.Entity;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty; 
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "User"; 
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public string? Country { get; set; }             
    public string? Organization { get; set; }        
    public string? ProfilePictureUrl { get; set; }         
    public long UserRating { get; set; } = 1500;  
    public long MaxRating { get; set; } = 1500;   
    public int SolvedProblemsCount { get; set; } = 0;

    // Navigation Properties
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();   
}