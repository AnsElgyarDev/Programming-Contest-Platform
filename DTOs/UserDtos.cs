namespace  Programming_Contest_Platform.DTO;

public record RegisterUserDto
(
    string UserEmail, 
    string UserPassword,
    string UserName
);

public record SignInUserDto
(
    string UserEmail, 
    string UserPassword
);