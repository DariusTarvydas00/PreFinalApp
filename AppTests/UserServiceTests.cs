using App.Services;
using DataAccess.IRepositories;
using DataAccess.Models;
using Moq;

namespace AppTests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _userService = new UserService(_mockRepo.Object);
    }

    [Fact]
    public void FindAll_ReturnsAllUsers()
    {
        var users = new List<User> { new User(), new User() };
        _mockRepo.Setup(repo => repo.FindAll()).Returns(users);

        var result = _userService.FindAll();

        Assert.Equal(users, result);
    }

    [Fact]
    public void GetById_ReturnsUser_WhenUserExists()
    {
        var user = new User();
        _mockRepo.Setup(repo => repo.GetById(1)).Returns(user);

        var result = _userService.GetById(1);

        Assert.Equal(user, result);
    }

    [Fact]
    public void GetById_ReturnsNull_WhenUserDoesNotExist()
    {
        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((User)null);

        var result = _userService.GetById(999);

        Assert.Null(result);
    }

    [Fact]
    public void GetByUserName_ReturnsUser_WhenUserExists()
    {
        var user = new User();
        _mockRepo.Setup(repo => repo.GetByUserName("username")).Returns(user);

        var result = _userService.GetByUserName("username");

        Assert.Equal(user, result);
    }

    [Fact]
    public void GetByUserName_ReturnsNull_WhenUserDoesNotExist()
    {
        _mockRepo.Setup(repo => repo.GetByUserName(It.IsAny<string>())).Returns((User)null);

        var result = _userService.GetByUserName("nonexistent");

        Assert.Null(result);
    }

    [Fact]
    public void Create_ThrowsArgumentNullException_WhenUserIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _userService.Create(null));
    }

    [Fact]
    public void Create_ReturnsUser_WhenUserIsValid()
    {
        var user = new User();
        _mockRepo.Setup(repo => repo.Create(user)).Returns(user);

        var result = _userService.Create(user);

        Assert.Equal(user, result);
    }

    [Fact]
    public void Update_ThrowsArgumentNullException_WhenUserIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _userService.Update(null));
    }

    [Fact]
    public void Update_ReturnsUpdatedUser_WhenUserIsValid()
    {
        var user = new User();
        _mockRepo.Setup(repo => repo.Update(user)).Returns(user);

        var result = _userService.Update(user);

        Assert.Equal(user, result);
    }

    [Fact]
    public void Delete_ReturnsDeletedUser()
    {
        var user = new User();
        _mockRepo.Setup(repo => repo.Delete(1)).Returns(user);

        var result = _userService.Delete(1);

        Assert.Equal(user, result);
    }
}