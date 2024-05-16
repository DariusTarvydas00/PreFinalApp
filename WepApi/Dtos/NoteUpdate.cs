
namespace WepApi.Dtos;

public class NoteUpdate
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public int CategoryId { get; set; }
    public string PhotoPath { get; set; }
}