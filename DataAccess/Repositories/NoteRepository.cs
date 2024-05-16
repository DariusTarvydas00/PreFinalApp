using DataAccess.IRepositories;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class NoteRepository(MainDbContext mainDbContext) : INoteRepository
{
    public List<Note?> FindAll(int userId)
    {
        return mainDbContext.Notes.Where(c => c != null && c.UserId == userId).ToList();
    }

    public List<Note?> FindAllByContent(string text,int userId)
    {
        return mainDbContext.Notes.Where(note => note != null && note.Text.Contains(text) | note.Title.Contains(text) && note.UserId == userId).ToList();
    }

    public Note? GetById(int id,int userId)
    {
        return mainDbContext.Notes.FirstOrDefault(note => note != null && note.Id == id && note.UserId == userId);
    }

    public Note? Create(Note? entity)
    {
        var note = mainDbContext.Notes.Add(entity);
        mainDbContext.SaveChanges();
        return note.Entity;
    }

    public Note? Update(Note entity)
    {
        var cat = mainDbContext.Notes.FirstOrDefault(note => note != null && note.Id == entity.Id && note.UserId == entity.UserId);
        if (cat != null)
        {
            cat.PhotoPath = entity.PhotoPath;
            cat.Text = entity.Text;
            cat.Title = entity.Title;
            cat.CategoryId = entity.CategoryId;
        }
        mainDbContext.SaveChanges();
        return cat;
    }

    public Note Delete(int id,int userId)
    {
        var cat = mainDbContext.Notes.FirstOrDefault(note => note != null && note.UserId == userId && note.Id == id);
        if (cat == null)
        {
            throw new InvalidDataException("Note does not exist with such Id");
        }
        mainDbContext.Notes.Remove(cat);
        mainDbContext.SaveChanges();
        return cat;
    }
}