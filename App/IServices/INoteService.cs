using App.Dtos;
using DataAccess.Models;

namespace App.IServices;

public interface INoteService
{
    List<NoteDto> FindAll(int userId);
    List<NoteDto> FindAllByContent(string text, int userId);
    NoteDto? GetById(int id,int userId);
    NoteDto? Create(NoteCreateDto? model, int userId);
    NoteDto? Update(NoteUpdateDto? model, int userId);
    NoteDto? Delete(int id,int userId);
}