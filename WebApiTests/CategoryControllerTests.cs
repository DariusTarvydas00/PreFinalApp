using System.Security.Claims;
using App;
using App.Dtos;
using App.IServices;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WepApi.Controllers;

namespace WebApiTests;

public class CategoryControllerTests
{
    private readonly Mock<ICategoryService> _mockCategoryService;
    private readonly CategoryController _controller;
    private readonly List<Category> _categories;

    public CategoryControllerTests()
    {
        _mockCategoryService = new Mock<ICategoryService>();
        _controller = new CategoryController(_mockCategoryService.Object);
        _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Work", UserId = 1 },
            new Category { Id = 2, Name = "Home", UserId = 1 }
        };

        // Setup the HttpContext to mimic User Identity and Claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "1")
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };
    }

    // [Fact]
    // public void GetAll_ReturnsAllCategoriesForUser()
    // {
    //     _mockCategoryService.Setup(s => s.FindAll(1)).Returns(_categories);
    //
    //     var result = _controller.GetAll();
    //
    //     var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //     var returnedCategories = Assert.IsType<List<Category>>(okResult.Value);
    //     Assert.Equal(2, returnedCategories.Count);
    // }

    // [Fact]
    // public void GetById_ReturnsCategory_WhenCategoryExists()
    // {
    //     _mockCategoryService.Setup(s => s.GetById(1, 1)).Returns(_categories[0]);
    //
    //     var result = _controller.GetById(1);
    //
    //     var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //     var returnedCategory = Assert.IsType<Category>(okResult.Value);
    //     Assert.Equal("Work", returnedCategory.Name);
    // }

    // [Fact]
    // public void Create_ReturnsCreatedCategory_WhenModelIsValid()
    // {
    //     var newCategory = new Category { Name = "New Category", UserId = 1 };
    //     _mockCategoryService.Setup(s => s.Create(It.IsAny<Category>())).Returns(newCategory);
    //
    //     var result = _controller.Create(new CategoryDto { Name = "New Category" });
    //
    //     var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //     var returnedCategory = Assert.IsType<Category>(okResult.Value);
    //     Assert.Equal("New Category", returnedCategory.Name);
    // }

    // [Fact]
    // public void Update_ReturnsUpdatedCategory_WhenModelIsValid()
    // {
    //     var updatedCategory = new CategoryDto() {  Name = "Updated Name" };
    //     _mockCategoryService.Setup(s => s.Update(It.IsAny<CategoryDto>())).Returns(updatedCategory);
    //
    //     var result = _controller.Update(new CategoryUpdateDto { Id = 2, Name = "Updated Name" });
    //
    //     var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //     var returnedCategory = Assert.IsType<Category>(okResult.Value);
    //     Assert.Equal("Updated Name", returnedCategory.Name);
    // }

    // [Fact]
    // public void Delete_ReturnsDeletedCategory_WhenCategoryExists()
    // {
    //     _mockCategoryService.Setup(s => s.Delete(1, 1)).Returns(_categories[0]);
    //
    //     var result = _controller.Delete(1);
    //
    //     var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //     Assert.Equal(_categories[0], okResult.Value);
    // }
}