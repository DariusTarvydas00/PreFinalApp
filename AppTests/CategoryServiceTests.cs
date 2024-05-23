using App.Services;
using DataAccess.IRepositories;
using DataAccess.Models;
using Moq;

namespace AppTests;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _mockRepo;
    private readonly CategoryService _categoryService;

    public CategoryServiceTests()
    {
        _mockRepo = new Mock<ICategoryRepository>();
        _categoryService = new CategoryService(_mockRepo.Object);
    }

    [Fact]
    public void FindAll_ReturnsAllCategoriesForUser()
    {
        var categories = new List<Category> { new Category(), new Category() };
        _mockRepo.Setup(repo => repo.FindAll(1)).Returns(categories);

        var result = _categoryService.FindAll(1);

        Assert.Equal(categories, result);
    }

    [Fact]
    public void FindAllByContent_ReturnsFilteredCategories()
    {
        var categories = new List<Category> { new Category { Name = "test" } };
        _mockRepo.Setup(repo => repo.FindAllByContent("test", 1)).Returns(categories);

        var result = _categoryService.FindAllByContent("test", 1);

        Assert.Equal(categories, result);
    }

    [Fact]
    public void GetById_ReturnsCategory_WhenCategoryExists()
    {
        var category = new Category();
        _mockRepo.Setup(repo => repo.GetById(1, 1)).Returns(category);

        var result = _categoryService.GetById(1, 1);

        Assert.Equal(category, result);
    }

    [Fact]
    public void GetById_ReturnsNull_WhenCategoryDoesNotExist()
    {
        _mockRepo.Setup(repo => repo.GetById(It.IsAny<int>(), It.IsAny<int>())).Returns((Category)null);

        var result = _categoryService.GetById(999, 1);

        Assert.Null(result);
    }

    [Fact]
    public void Create_ThrowsArgumentNullException_WhenCategoryIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _categoryService.Create(null));
    }

    [Fact]
    public void Create_ReturnsCategory_WhenCategoryIsValid()
    {
        var category = new Category();
        _mockRepo.Setup(repo => repo.Create(category)).Returns(category);

        var result = _categoryService.Create(category);

        Assert.Equal(category, result);
    }

    [Fact]
    public void Update_ThrowsArgumentNullException_WhenCategoryIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _categoryService.Update(null));
    }

    [Fact]
    public void Update_ReturnsUpdatedCategory_WhenCategoryIsValid()
    {
        var category = new Category();
        _mockRepo.Setup(repo => repo.Update(category)).Returns(category);

        var result = _categoryService.Update(category);

        Assert.Equal(category, result);
    }

    [Fact]
    public void Delete_ReturnsDeletedCategory()
    {
        var category = new Category();
        _mockRepo.Setup(repo => repo.Delete(1, 1)).Returns(category);

        var result = _categoryService.Delete(1, 1);

        Assert.Equal(category, result);
    }
}