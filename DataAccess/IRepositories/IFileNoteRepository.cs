using DataAccess.Models;

namespace DataAccess.IRepositories;

public interface IFileNoteRepository
{
    Task<FileNote> UploadFile(FileNote file);
    FileNote Delete(int id, int userId);
}