using System.Security.Claims;
using App.Dtos;
using App.IServices;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WepApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class FileNoteController : ControllerBase
{
    private readonly IFileNoteService _fileNoteService;

    public FileNoteController(IFileNoteService fileNoteService)
    {
        _fileNoteService = fileNoteService;
    }

    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadFile(IFormFile? file, int noteId)
    {
        if (file == null || file.Length <= 0)
        {
            return BadRequest("File is not selected or empty.");
        }

        var uploadedFile = await _fileNoteService.UploadFile(file,GetUserIdFromClaims(),noteId);

        return Ok(uploadedFile);
    }
    
    [HttpDelete("{id:int}")]
    public ActionResult<FileNoteDto> Delete(int id)
    {
        try
        {
            var delete = _fileNoteService.Delete(id,GetUserIdFromClaims());
            return Ok(delete);
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