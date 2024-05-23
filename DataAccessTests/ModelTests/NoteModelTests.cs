using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace DataAccessTests.ModelTests;

public class NoteModelTests
{
    private readonly ValidationContext _validationContext;
    private readonly Note _note;

    public NoteModelTests()
    {
        _note = new Note { Id = 1, Title = "ValidTitle123", Text = "This is a valid content up to 400 characters long.", UserId = 1, CategoryId = 1 };
        _validationContext = new ValidationContext(_note, null, null);
    }

    [Fact]
    public void Note_WithValidProperties_PassesValidation()
    {
        var validationResult = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(_note, _validationContext, validationResult, true);

        Assert.True(isValid);
    }

    [Fact]
    public void Note_WithInvalidTitle_FailsValidation()
    {
        _note.Title = "Invalid Title!";
        var validationResult = new List<ValidationResult>();
        Validator.TryValidateObject(_note, _validationContext, validationResult, true);

        Assert.NotEmpty(validationResult);
        Assert.Contains(validationResult, v => v.ErrorMessage.Contains("Only letters and digits are allowed"));
    }
}