namespace App.Dtos;

public class NoteCreateDto
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int CategoryId { get; set; }
    
}