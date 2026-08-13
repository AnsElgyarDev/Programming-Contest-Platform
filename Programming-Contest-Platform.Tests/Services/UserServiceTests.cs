using System.Runtime.InteropServices.Marshalling;
using Azure.Core.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Moq;
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
    public async Task GetUserProfileAsync_WhenUserExists_ShouldReturnUser()
    {
        Guid UserId = Guid.NewGuid();
        string? FullName = "Ans Elgyar";
        string PasswordHash = "";
        string Role = "User";
        string encryptedName = "ABCD";
        string decryptedName = "Ans Taher";
        var user = new User
        {
            Id = UserId,
            FullName = FullName,
            Username = encryptedName,
            PasswordHash = PasswordHash, 
            Role = Role
        };

        _encryptionServiceMock
                .Setup(repo => repo.Encrypt(encryptedName))
                .Returns(decryptedName);

        user.Username = decryptedName;

        
        Assert.Equal(user.Username, encryptedName);
        Assert.Equal(user.Username, encryptedName);

    }
    
}