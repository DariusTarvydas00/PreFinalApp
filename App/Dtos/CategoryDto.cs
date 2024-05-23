using DataAccess.Models;

namespace App.Dtos;

public class CategoryDto
{
    public string Name { get; set; }
    public List<NoteDto> Notes { get; set; }
}