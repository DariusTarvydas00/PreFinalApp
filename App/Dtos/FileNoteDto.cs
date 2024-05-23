namespace App.Dtos;

public class FileNoteDto
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public int NoteId { get; set; }
    public int UserId { get; set; }
}