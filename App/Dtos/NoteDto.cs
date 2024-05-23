using DataAccess.Models;

namespace App.Dtos;

public class NoteDto
{
    public string Title { get; set; }
    public string Text { get; set; }
    public List<FileNoteDto> FileNotes { get; set; }
}