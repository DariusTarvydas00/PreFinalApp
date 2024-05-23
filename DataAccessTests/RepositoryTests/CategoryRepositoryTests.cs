using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTests.RepositoryTests;

public class CategoryRepositoryTests : IDisposable
{
    private readonly MainDbContext _dbContext;
    private readonly CategoryRepository _categoryRepository;

    public CategoryRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _dbContext = new MainDbContext(options);
        _categoryRepository = new CategoryRepository(_dbContext);
        InitializeData();
    }

    private void InitializeData()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Categories.AddRange(
            new Category { Id = 1, Name = "Test1", UserId = 1 },
            new Category { Id = 2, Name = "Test2", UserId = 1 }
        );
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Fact]
    public void FindAll_ReturnsAllCategoriesForUser()
    {
        var result = _categoryRepository.FindAll(1);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetById_ReturnsCategory_WhenCategoryExists()
    {
        var result = _categoryRepository.GetById(1, 1);
        Assert.NotNull(result);
        Assert.Equal(1, result?.Id);
    }

    [Fact]
    public void GetById_ReturnsNull_WhenCategoryDoesNotExist()
    {
        var result = _categoryRepository.GetById(999, 1);
        Assert.Null(result);
    }

    [Fact]
    public void Create_AddsCategory_ReturnsCategory()
    {
        var newCategory = new Category { Name = "New", UserId = 3 };
        var result = _categoryRepository.Create(newCategory);

        Assert.NotNull(result);
        Assert.Equal("New", result?.Name);
        Assert.Equal(3, result?.UserId);
    }

    [Fact]
    public void Update_UpdatesCategory_ReturnsUpdatedCategory()
    {
        var cat = _categoryRepository.GetById(2, 1);
        cat.Name = "Updated";
        var result = _categoryRepository.Update(cat);

        Assert.NotNull(result);
        Assert.Equal("Updated", result?.Name);
    }

    [Fact]
    public void Delete_RemovesCategory_ReturnsDeletedCategory()
    {
        var result = _categoryRepository.Delete(1, 1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.False(_dbContext.Categories.Any(c => c.Id == 1));
    }
}
