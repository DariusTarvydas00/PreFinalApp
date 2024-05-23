using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTests.RepositoryTests;

public class UserRepositoryTests : IDisposable
{
    private readonly MainDbContext _dbContext;
    private readonly UserRepository _userRepository;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "UserTestDb")
            .Options;
        _dbContext = new MainDbContext(options);
        _userRepository = new UserRepository(_dbContext);
        InitializeData();
    }

    private void InitializeData()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Users.AddRange(
            new User { Id = 1, UserName = "JohnDoe", PasswordHash = [123,123], PasswordSalt = [3,1,2]},
            new User { Id = 2, UserName = "JaneDoe", PasswordHash = [123,123], PasswordSalt = [3,1,2] }
        );
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Fact]
    public void FindAll_ReturnsAllUsers()
    {
        var result = _userRepository.FindAll();
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetById_ReturnsUser_WhenUserExists()
    {
        var result = _userRepository.GetById(1);
        Assert.NotNull(result);
        Assert.Equal("JohnDoe", result?.UserName);
    }

    [Fact]
    public void GetById_ReturnsNull_WhenUserDoesNotExist()
    {
        var result = _userRepository.GetById(999);
        Assert.Null(result);
    }

    [Fact]
    public void GetByUserName_ReturnsUser_WhenUserExists()
    {
        var result = _userRepository.GetByUserName("JaneDoe");
        Assert.NotNull(result);
        Assert.Equal(2, result?.Id);
    }

    [Fact]
    public void GetByUserName_ReturnsNull_WhenUserDoesNotExist()
    {
        var result = _userRepository.GetByUserName("NonExistentUser");
        Assert.Null(result);
    }

    [Fact]
    public void Create_AddsUser_ReturnsUser()
    {
        var newUser = new User { UserName = "NewUser", PasswordHash = [1,1,1], PasswordSalt = [3,3,3]};
        var result = _userRepository.Create(newUser);

        Assert.NotNull(result);
        Assert.Equal("NewUser", result?.UserName);
    }

    [Fact]
    public void Update_UpdatesUser_ReturnsUpdatedUser()
    {
        var user = _userRepository.GetById(1);
        user.UserName = "UpdatedJohn";
        var result = _userRepository.Update(user);

        Assert.NotNull(result);
        Assert.Equal("UpdatedJohn", result?.UserName);
    }

    [Fact]
    public void Delete_RemovesUser_ReturnsDeletedUser()
    {
        var result = _userRepository.Delete(1);
        _dbContext.SaveChanges();

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.False(_dbContext.Users.Any(u => u.Id == 1));
    }
}
