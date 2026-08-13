using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
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
        // Arrange 
        Guid? userId = null;

        // Act 
        var result = await _sut.GetUserProfileAsync(userId ?? Guid.Empty);

        // Assert 
        Assert.Null(result);
    }
    
}