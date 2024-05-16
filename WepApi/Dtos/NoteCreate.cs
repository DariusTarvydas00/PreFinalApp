namespace WepApi.Dtos;

public class NoteCreate
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int CategoryId { get; set; }
    public string PhotoPath { get; set; }
}