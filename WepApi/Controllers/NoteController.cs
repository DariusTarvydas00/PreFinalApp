using System.Security.Claims;
using App.Dtos;
using App.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WepApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NoteController(INoteService noteService) : Controller
{
    [HttpGet]
    public ActionResult<IEnumerable<NoteDto>> GetAll()
    {
        try
        {
            return Ok(noteService.FindAll(GetUserIdFromClaims()));
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
    
    [HttpGet("content/{text}")]
    public ActionResult<IEnumerable<NoteDto>> GetAllByContent(string text)
    {
        try
        {
            return Ok(noteService.FindAllByContent(text,GetUserIdFromClaims()));
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<NoteDto> GetById(int id)
    {
        try
        {
            var cat = noteService.GetById(id, GetUserIdFromClaims());
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
    public ActionResult<NoteDto> Create([FromBody]NoteCreateDto? model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model != null)
            {
                var createdNote = noteService.Create(model,GetUserIdFromClaims());
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
    public ActionResult<NoteDto> Update([FromBody]NoteUpdateDto? model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model != null)
            {
                var updatedNote = noteService.Update(model,GetUserIdFromClaims());
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
    public ActionResult<NoteDto> Delete(int id)
    {
        try
        {
            var deletedNote = noteService.Delete(id,GetUserIdFromClaims());
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