using App.IServices;
using App.Jwt;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WepApi.Controllers;

namespace WebApiTests;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IJwtService> _mockJwtService;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockJwtService = new Mock<IJwtService>();
        _controller = new UserController(_mockUserService.Object, _mockJwtService.Object);
    }

    [Fact]
    public void SignUp_UserExists_ReturnsConflict()
    {
        var username = "existingUser";
        var password = "password123";

        _mockUserService.Setup(x => x.GetByUserName(username)).Returns(new User());

        var result = _controller.SignUp(username, password);

        Assert.IsType<ConflictObjectResult>(result);
    }

    [Fact]
    public void LogIn_InvalidCredentials_ReturnsUnauthorized()
    {
        var username = "user";
        var password = "wrongPassword";

        _mockUserService.Setup(x => x.GetByUserName(username)).Returns(new User { UserName = username, PasswordHash = new byte[0], PasswordSalt = new byte[0] });
        _mockJwtService.Setup(x => x.VerifyPassword(password, It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(false);

        var result = _controller.LogIn(username, password);

        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}