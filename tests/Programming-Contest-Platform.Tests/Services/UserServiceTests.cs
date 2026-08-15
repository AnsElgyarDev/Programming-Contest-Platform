using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Services;
using System.Globalization;
using Programming_Contest_Platform.DTO;

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
        var result = await _sut.GetUserProfileAsync(userId);
        
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

    [Fact]
    public async Task DeleteUserAsync_WhenUserIsNull_ShouldReturnUserFailureServiceResult()
    {
        Guid nonExistedUserId = Guid.NewGuid(); 
        var result = await _sut.DeleteUserAsync(nonExistedUserId);
        
        Assert.Equal(result, new ServiceResult<string> 
        {
            isFailure = true,
            ErrorMessage = "There is no User With This ID!",
            isSuccess = false
        }); 
    }
    
    [Fact]
    public async Task DeleteUserAsync_WhenUserExists_ShouldDeleteUserAndReturnSuccessServiceResult()
    {
        Guid userId = Guid.NewGuid();
        
        var userInDb = new User
        {
            Id = userId,
            FullName = "Ans Elgyar",
            Username = "anselgyar27", 
            PasswordHash = "hash123", 
            Role = "User"
        };

        _context.Users.Add(userInDb);
        await _context.SaveChangesAsync();
        
        var result = await _sut.DeleteUserAsync(userId);
        
        Assert.Equal(result, new ServiceResult<string>
        {
            Data = "The Deletion Completed Successfully!",
            isSuccess = true
        });

        var userInDbAfterDelete = await _context.Users.FindAsync(userId);
     
        Assert.Null(userInDbAfterDelete);
    }

    [Fact]
    public async Task UpdateUserAsync_WhenUserDoesNotExist_ShouldReturnFailureResult()
    {
        // 1. ARRANGE
        Guid nonExistentUserId = Guid.NewGuid();
        var updateDto = new UpdateUserDto
        {
            FullName = "New Name",
            Country = "Egypt"
        };

        var result = await _sut.UpdateUserAsync(nonExistentUserId, updateDto);

        Assert.False(result.isSuccess);
        Assert.Equal("There is No User With This ID !", result.ErrorMessage);
    }

    [Theory]
    [InlineData("Updated Name", "Egypt", "Damanhour Univ", "http://new-pic.com/1.png")]
    [InlineData("Updated Name Only", null, null, null)]
    [InlineData(null, "Canada", "SalamDev", null)]
    public async Task UpdateUserAsync_WhenUserExists_ShouldUpdateProvidedFieldsAndKeepExistingData(
        string? newFullName, 
        string? newCountry, 
        string? newOrganization, 
        string? newProfilePictureUrl)
    {
        Guid userId = Guid.NewGuid();
        
        var initialUser = new User
        {
            Id = userId,
            FullName = "Original Name",
            Country = "Original Country",
            Organization = "Original Org",
            ProfilePictureUrl = "http://original.com/pic.png",
            Username = "ans27",
            PasswordHash = "hash123",
            Role = "User"
        };

        _context.Users.Add(initialUser);
        await _context.SaveChangesAsync();

        var updateDto = new UpdateUserDto
        {
            FullName = newFullName,
            Country = newCountry,
            Organization = newOrganization,
            ProfilePictureUrl = newProfilePictureUrl
        };

        var result = await _sut.UpdateUserAsync(userId, updateDto);

        Assert.True(result.isSuccess);
        Assert.Equal("Updated User Successfully!", result.Data);

        var updatedUserInDb = await _context.Users.FindAsync(userId);
        Assert.NotNull(updatedUserInDb);

        Assert.Equal(newFullName ?? "Original Name", updatedUserInDb.FullName);
        Assert.Equal(newCountry ?? "Original Country", updatedUserInDb.Country);
        Assert.Equal(newOrganization ?? "Original Org", updatedUserInDb.Organization);
        Assert.Equal(newProfilePictureUrl ?? "http://original.com/pic.png", updatedUserInDb.ProfilePictureUrl);
    } 
}