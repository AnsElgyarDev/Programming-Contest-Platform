using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Services;

public class UserTests
{
    private readonly AppDbContext _context;
    private readonly Mock<IEncryptionService> _encryptionServiceMock;
    private readonly UserService _sut;

    public UserTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);

        _encryptionServiceMock = new Mock<IEncryptionService>();

        _sut = new UserService(_context, _encryptionServiceMock.Object);
    }

    [Fact]
    public async Task GetUserProfileAsync_WhenUserIdIsNull_ShouldReturnNull()
    {
        Guid? userId = null;         
        var result = await _sut.GetUserProfileAsync(userId ?? Guid.Empty);
        
        Assert.Null(result);
    }

    [Fact]
    public async Task GetUserProfileAsync_WhenUserExists_ShouldReturnUserWithDecryptedName()
    {
        // 1. ARRANGE
        Guid userId = Guid.NewGuid();
        string encryptedUsername = "ABCD";
        string decryptedUsername = "Ans Taher";

        var userInDb = new User
        {
            Id = userId,
            FullName = "Ans Elgyar",
            Username = encryptedUsername, 
            PasswordHash = "hash123", 
            Role = "User"
        };

        _context.Users.Add(userInDb);
        await _context.SaveChangesAsync();

        _encryptionServiceMock
            .Setup(x => x.Decrypt(encryptedUsername))
            .Returns(decryptedUsername);

        // 2. ACT
        var result = await _sut.GetUserProfileAsync(userId);

        // 3. ASSERT
        Assert.NotNull(result);
        Assert.Equal(decryptedUsername, result.Username); 
    }
}