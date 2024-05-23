using App.Dtos;
using AutoMapper;
using DataAccess.Models;

namespace App.Extensions;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<NoteDto, Note>()
            .ForMember(dest => dest.Title, opt => opt
                .MapFrom(src => src.Title))
            .ForMember(dest => dest.Text, opt => opt
                .MapFrom(src => src.Text))
            .ForMember(dest => dest.FileNotes, opt => opt
                .MapFrom(src => src.FileNotes)).ReverseMap();
        
        CreateMap<NoteCreateDto, Note>()
            .ForMember(dest => dest.Title, opt => opt
                .MapFrom(src => src.Title))
            .ForMember(dest => dest.Text, opt => opt
                .MapFrom(src => src.Text)).ReverseMap();

        CreateMap<NoteUpdateDto, Note>()
            .ForMember(dest => dest.Id, opt => opt
                .MapFrom(src => src.Id))
            .ForMember(dest => dest.Text, opt => opt
                .MapFrom(src => src.Text))
            .ForMember(dest => dest.Title, opt => opt
                .MapFrom(src => src.Title));
        
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Name, opt => opt
                .MapFrom(src => src.Name))
            .ForMember(dest => dest.Notes, opt => opt
                .MapFrom(src => src.Notes)).ReverseMap();
        
        CreateMap<CategoryCreateDto, Category>()
            .ForMember(dest => dest.Name, opt => opt
                .MapFrom(src => src.Name)).ReverseMap();
        
        CreateMap<CategoryUpdateDto, Category>()
            .ForMember(dest => dest.Name, opt => opt
                .MapFrom(src => src.Name)).ReverseMap()  
            .ForMember(dest => dest.Id, opt => opt
                .MapFrom(src => src.Id)).ReverseMap();
        
        CreateMap<FileNoteDto, FileNote>()
            .ForMember(dest => dest.FileName, opt => opt
                .MapFrom(src => src.FileName)) 
            .ForMember(dest => dest.NoteId, opt => opt
                .MapFrom(src => src.NoteId))
            .ForMember(dest => dest.FilePath, opt => opt
                .MapFrom(src => src.FilePath)).ReverseMap();
    }
}