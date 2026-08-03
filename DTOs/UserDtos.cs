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