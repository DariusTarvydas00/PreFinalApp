using DataAccess.IRepositories;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class FileNoteRepository : IFileNoteRepository
{
    private readonly MainDbContext _context;

    public FileNoteRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<FileNote> UploadFile(FileNote file)
    {
        _context.FileNotes.Add(file);
        await _context.SaveChangesAsync();
        return file;
    }

    public FileNote Delete(int id, int userId)
    {
        var fileNote = _context.FileNotes.FirstOrDefault(note => note.Id == id && note.UserId == userId);
        _context.FileNotes.Remove(fileNote);
        _context.SaveChanges();
        return fileNote;
    }
}