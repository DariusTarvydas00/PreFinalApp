﻿
namespace App.Dtos;

public class NoteUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public int CategoryId { get; set; }
}