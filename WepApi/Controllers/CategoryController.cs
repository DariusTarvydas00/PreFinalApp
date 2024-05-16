using System.Security.Claims;
using App.IServices;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WepApi.Dtos;

namespace WepApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController(ICategoryService categoryService) : Controller
{
    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetAll()
    {
        try
        {
            var userId = GetUserIdFromClaims();
            var list = categoryService.FindAll(userId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
    
    [HttpGet("content/{text}")]
    public ActionResult<IEnumerable<Category>> GetAllByContent(string text)
    {
        try
        {
            var userId = GetUserIdFromClaims();
            var list = categoryService.FindAllByContent(text, userId);
            return Ok(list);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<Category> GetById(int id)
    {
        try
        {
            var userId = GetUserIdFromClaims();
            var cat = categoryService.GetById(id, userId);
            if (cat != null)
            {
                return Ok(cat);
            }

            return BadRequest();
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPost]
    public ActionResult<Category> Create([FromBody]CategoryCreate? model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserIdFromClaims();
            if (model != null)
            {
                var newCategory = new Category()
                {
                    UserId = userId,
                    Name = model.Name
                };
                var createdCategory = categoryService.Create(newCategory);
                return Ok(createdCategory);
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut]
    public ActionResult<Category> Update([FromBody]CategoryUpdate? model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserIdFromClaims();
            if (model != null)
            {
                var updateModel = new Category()
                {
                    UserId = userId,
                    Id = model.Id,
                    Name = model.Name
                };
                var updatedCategory = categoryService.Update(updateModel);
                return Ok(updatedCategory);
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Category> Delete(int id)
    {
        try
        {
            var userId = GetUserIdFromClaims();
            var deletedCategory = categoryService.Delete(id,userId);
            if (deletedCategory != null)
            {
                return Ok(deletedCategory);
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
      
    }
    
    [Authorize]
    private int GetUserIdFromClaims()
    {
        try
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            throw new InvalidOperationException("User ID claim is missing or invalid.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }
}