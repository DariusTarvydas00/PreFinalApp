using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace DataAccessTests.ModelTests;

public class UserModelTests
{
    private readonly ValidationContext _validationContext;
    private readonly User _user;

    public UserModelTests()
    {
        _user = new User { Id = 1, UserName = "ValidUser123", PasswordHash = new byte[64], PasswordSalt = new byte[128] };
        _validationContext = new ValidationContext(_user, null, null);
    }

    [Fact]
    public void User_WithValidProperties_PassesValidation()
    {
        var validationResult = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(_user, _validationContext, validationResult, true);

        Assert.True(isValid);
    }

    [Fact]
    public void User_WithEmptyUserName_FailsValidation()
    {
        _user.UserName = string.Empty;
        var validationResult = new List<ValidationResult>();
        Validator.TryValidateObject(_user, _validationContext, validationResult, true);

        Assert.NotEmpty(validationResult);
        Assert.Contains(validationResult, v => v.ErrorMessage.Contains("The UserName field is required"));
    }
}