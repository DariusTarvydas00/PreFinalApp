using App.IServices;
using DataAccess.Models;
using Jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WepApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(IUserService userService, IJwtService jwtService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("SignUp")]
    public IActionResult SignUp(string username, string password)
    {
        try
        {
            if (GetByUserName(username) != null) return Conflict("User already exists");
            var user = jwtService.CreateUser(username, password);
            userService.Create(user);
            return Ok(new { message = "User created successfully" });
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [Authorize]
    private User? GetByUserName(string username)
    {
        try
        {
            return userService.GetByUserName(username);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("LogIn")]
    public IActionResult LogIn(string username, string password)
    {
        try
        {
            var user = GetByUserName(username);
            if (user == null || !jwtService.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized("Invalid username or password");
            var token = jwtService.GenerateJwtToken(username,user.Id);
            return Ok(new { token });
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
}