using App.Dtos;
using App.IServices;
using AutoMapper;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;

namespace App.Services;

public class FileNoteService(IFileNoteRepository fileNoteRepository, IMapper mapper) : IFileNoteService
{

    public async Task<FileNoteDto> UploadFile(IFormFile file, int userId, int noteId)
    {
        var fileNote = mapper.Map<FileNote>(new FileNoteDto() {UserId = userId,NoteId = noteId, FileName = file.FileName, FilePath = "uploads/"+file.FileName});
        return mapper.Map<FileNoteDto>(await fileNoteRepository.UploadFile(fileNote));
    }

    public FileNoteDto Delete(int id, int userId)
    {
        var deleted = fileNoteRepository.Delete(id,userId);
        return mapper.Map<FileNoteDto>(deleted);
    }
}