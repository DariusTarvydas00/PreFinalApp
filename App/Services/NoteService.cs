using App.IServices;
using DataAccess.IRepositories;
using DataAccess.Models;

namespace App.Services;

public class NoteService(INoteRepository noteRepository) : INoteService
{
    public List<Note?> FindAll(int userId)
    {
        return noteRepository.FindAll(userId);
    }

    public List<Note?> FindAllByContent(string text,int userId)
    {
        return noteRepository.FindAllByContent(text,userId);
    }

    public Note? GetById(int id,int userId)
    {
        return noteRepository.GetById(id,userId);
    }

    public Note? Create(Note? model)
    {
        return noteRepository.Create(model ?? throw new ArgumentNullException(nameof(model)));
    }

    public Note? Update(Note? model)
    {
        return noteRepository.Update(model ?? throw new ArgumentNullException(nameof(model)));
    }

    public Note Delete(int id,int userId)
    {
        return noteRepository.Delete(id,userId);
    }
}