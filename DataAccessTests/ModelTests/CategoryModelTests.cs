using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace DataAccessTests.ModelTests;

public class CategoryModelTests
{
    private readonly ValidationContext _validationContext;
    private readonly Category _category;

    public CategoryModelTests()
    {
        _category = new Category { Id = 1, Name = "ValidName123", UserId = 1, User = new User(), Notes = new List<Note>() };
        _validationContext = new ValidationContext(_category, null, null);
    }

    [Fact]
    public void Category_WithValidName_PassesValidation()
    {
        var validationResult = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(_category, _validationContext, validationResult, true);

        Assert.True(isValid);
    }

    [Fact]
    public void Category_WithInvalidName_FailsValidation()
    {
        _category.Name = "Invalid Name!";
        var validationResult = new List<ValidationResult>();
        Validator.TryValidateObject(_category, _validationContext, validationResult, true);

        Assert.NotEmpty(validationResult);
        Assert.Contains(validationResult, v => v.ErrorMessage.Contains("Only letters and digits are allowed"));
    }
}