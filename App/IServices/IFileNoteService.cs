using App.Dtos;
using Microsoft.AspNetCore.Http;

namespace App.IServices;

public interface IFileNoteService
{
    Task<FileNoteDto> UploadFile(IFormFile file, int userId, int noteId);
    FileNoteDto Delete(int id, int userId);
}