using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
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

        // 2. إنشاء الـ Mock للـ EncryptionService
        _encryptionServiceMock = new Mock<IEncryptionService>();

        // 3. حقن الـ Dependencies الصحيحة داخل الـ UserService
        _sut = new UserService(_context, _encryptionServiceMock.Object);
    }

    [Fact]
    public async Task GetUserProfileAsync_WhenUserIdIsNull_ShouldReturnNull()
    {
        // 1. Arrange

        UserProfileDto ExcepecteduserProfileDto = new UserProfileDto
        {
            Username = "Ans",
            FullName = "Ans Elgyar",
            SolvedProblemsCount = 12
        } ;


        // 2. Act
        
        // 3. Assert     
    }

    /*
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

    */

}