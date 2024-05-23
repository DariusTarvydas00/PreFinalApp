using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTests.RepositoryTests;

public class NoteRepositoryTests : IDisposable
{
    private readonly MainDbContext _dbContext;
    private readonly NoteRepository _noteRepository;

    public NoteRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "NoteTestDb")
            .Options;
        _dbContext = new MainDbContext(options);
        _noteRepository = new NoteRepository(_dbContext);
        InitializeData();
    }

    private void InitializeData()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Notes.AddRange(
            new Note { Id = 1,  Title = "Meeting Notes", Text = "Discuss project", UserId = 1 },
            new Note { Id = 2,  Title = "Grocery List", Text = "Eggs, Milk", UserId = 1 }
        );
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Fact]
    public void FindAll_ReturnsAllNotesForUser()
    {
        var result = _noteRepository.FindAll(1);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void FindAllByContent_ReturnsFilteredNotes()
    {
        var result = _noteRepository.FindAllByContent("project", 1);
        Assert.Single(result);
        Assert.Contains("Discuss project", result[0].Text);
    }

    [Fact]
    public void GetById_ReturnsNote_WhenNoteExists()
    {
        var result = _noteRepository.GetById(1, 1);
        Assert.NotNull(result);
        Assert.Equal(1, result?.Id);
    }

    [Fact]
    public void GetById_ReturnsNull_WhenNoteDoesNotExist()
    {
        var result = _noteRepository.GetById(999, 1);
        Assert.Null(result);
    }

    [Fact]
    public void Create_AddsNote_ReturnsNote()
    {
        var newNote = new Note { Title = "New Note", Text = "New content", UserId = 2 };
        var result = _noteRepository.Create(newNote);

        Assert.NotNull(result);
        Assert.Equal("New Note", result?.Title);
        Assert.Equal("New content", result?.Text);
    }

    [Fact]
    public void Update_UpdatesNote_ReturnsUpdatedNote()
    {
        var note = _noteRepository.GetById(2, 1);
        note.Text = "Updated List";
        var result = _noteRepository.Update(note);

        Assert.NotNull(result);
        Assert.Equal("Updated List", result?.Text);
    }

    [Fact]
    public void Delete_RemovesNote_ReturnsDeletedNote()
    {
        var result = _noteRepository.Delete(1, 1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.False(_dbContext.Notes.Any(n => n.Id == 1));
    }
}
