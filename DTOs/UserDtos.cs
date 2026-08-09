namespace  Programming_Contest_Platform.DTO;

public record UserDto
(
    string UserPassword,
    string UserName,
    string Role
);

public record UpdateUserDto
(
    string? FullName,
    string? Country,
    string? Organization,
    string? ProfilePictureUrl
);

public class UserProfileDto
{
    public string Username {get ;set; } = string.Empty;
    public string? FullName {get ;set; } = string.Empty;
    public string? Country {get ;set; } = string.Empty;
    public string? Organization {get ;set; } = string.Empty;
    public string? ProfilePictureUrl {get ;set; } = string.Empty;
    public long UserRating { get; set;}
    public long MaxRating {get ;set; }
    public int SolvedProblemsCount {get ;set; } 
}
