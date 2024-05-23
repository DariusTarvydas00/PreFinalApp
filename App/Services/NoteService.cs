using App.Dtos;
using App.IServices;
using AutoMapper;
using DataAccess.IRepositories;
using DataAccess.Models;

namespace App.Services;

public class NoteService(INoteRepository noteRepository, IMapper mapper, ICategoryRepository categoryRepository) : INoteService
{
    public List<NoteDto> FindAll(int userId)
    {
        var notes = noteRepository.FindAll(userId);
        return mapper.Map<List<NoteDto>>(notes);
    }

    public List<NoteDto> FindAllByContent(string text, int userId)
    {
        var notes = noteRepository.FindAllByContent(text,userId);
        return mapper.Map<List<NoteDto>>(notes);
    }

    public NoteDto? GetById(int id,int userId)
    {
        return mapper.Map<NoteDto>(noteRepository.GetById(id,userId));
    }

    public NoteDto? Create(NoteCreateDto? model, int userId)
    {
        var createNote = mapper.Map<Note>(model);
        createNote.UserId = userId;
        var category = categoryRepository.GetById(model.CategoryId,userId);
        if (category == null)
        {
            var newDefaultCategory = categoryRepository.Create(new Category(){UserId = userId, Name = "New Category"});
            createNote.CategoryId = newDefaultCategory.Id;
        }

        return mapper.Map<NoteDto>(noteRepository.Create(createNote ?? throw new ArgumentNullException(nameof(model))));
    }

    public NoteDto? Update(NoteUpdateDto? model, int userId)
    {
        var updateNote = mapper.Map<Note>(model);
        updateNote.UserId = userId;
        return mapper.Map<NoteDto>(noteRepository.Update(updateNote ?? throw new ArgumentNullException(nameof(model))));
    }

    public NoteDto Delete(int id,int userId)
    {
        return mapper.Map<NoteDto>(noteRepository.Delete(id,userId));
    }
}