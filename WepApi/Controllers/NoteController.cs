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
public class NoteController(INoteService noteService) : Controller
{
    [HttpGet]
    public ActionResult<IEnumerable<Note>> GetAll()
    {
        try
        {
            var userId = GetUserIdFromClaims();
            return Ok(noteService.FindAll(userId));
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
    
    [HttpGet("content/{text}")]
    public ActionResult<IEnumerable<Note>> GetAllByContent(string text)
    {
        try
        {
            var userId = GetUserIdFromClaims();
            return Ok(noteService.FindAllByContent(text,userId));
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<Note> GetById(int id)
    {
        try
        {
            var userId = GetUserIdFromClaims();
            var cat = noteService.GetById(id, userId);
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
    public ActionResult<Note> Create([FromBody]NoteCreate? model)
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
                var newNote = new Note()
                {
                    Title = model.Title,
                    Text = model.Text,
                    PhotoPath = model.PhotoPath,
                    CategoryId = model.CategoryId,
                    UserId = userId
                };
                var createdNote = noteService.Create(newNote);
                return Ok(createdNote);
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut]
    public ActionResult<Note> Update([FromBody]NoteUpdate? model)
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
                var updateModel = new Note()
                {
                    Id = model.Id,
                    Text = model.Text,
                    Title = model.Title,
                    PhotoPath = model.PhotoPath,
                    UserId = userId,
                    CategoryId = model.CategoryId
                };
                var updatedNote = noteService.Update(updateModel);
                return Ok(updatedNote);
            }
            return BadRequest();
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult<Note> Delete(int id)
    {
        try
        {
            var userId = GetUserIdFromClaims();
            var deletedNote = noteService.Delete(id,userId);
            if (deletedNote != null)
            {
                return Ok(deletedNote);
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
            throw new Exception(e.Message);
        }
    }
}