using System.Security.Claims;
using App.Dtos;
using App.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WepApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController(ICategoryService categoryService) : Controller
{
    [HttpGet]
    public ActionResult<IEnumerable<CategoryDto>> GetAll()
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
    public ActionResult<IEnumerable<CategoryDto>> GetAllByContent(string text)
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
    public ActionResult<CategoryDto> GetById(int id)
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
    public ActionResult<CategoryDto> Create([FromBody]CategoryCreateDto? model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model != null)
            {
                var createdCategory = categoryService.Create(model,GetUserIdFromClaims());
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
    public ActionResult<CategoryDto> Update([FromBody]CategoryUpdateDto? model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model != null)
            {
                var updatedCategory = categoryService.Update(model, GetUserIdFromClaims());
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
    public ActionResult<CategoryDto> Delete(int id)
    {
        try
        {
            var deletedCategory = categoryService.Delete(id,GetUserIdFromClaims());
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