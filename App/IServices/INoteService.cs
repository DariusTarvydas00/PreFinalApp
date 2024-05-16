using DataAccess.Models;

namespace App.IServices;

public interface INoteService
{
    List<Note?> FindAll(int userId);
    List<Note?> FindAllByContent(string text,int userId);
    Note? GetById(int id,int userId);
    Note? Create(Note? model);
    Note? Update(Note? model);
    Note? Delete(int id,int userId);
}