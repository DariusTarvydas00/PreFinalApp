using DataAccess.Models;

namespace DataAccess.IRepositories;

public interface INoteRepository
{
    List<Note?> FindAll(int userId);
    List<Note?> FindAllByContent(string text,int userId);
    Note? GetById(int id,int userId);
    Note? Create(Note? entity);
    Note? Update(Note entity);
    Note Delete(int id,int userId);
}