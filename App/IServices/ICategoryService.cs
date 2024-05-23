using App.Dtos;
using DataAccess.Models;

namespace App.IServices;

public interface ICategoryService
{
    List<CategoryDto> FindAll(int userIdClaim);
    List<CategoryDto> FindAllByContent(string text, int userIdClaim);
    CategoryDto? GetById(int id,int userIdClaim);
    CategoryDto? Create(CategoryCreateDto? model, int userId);
    CategoryDto? Update(CategoryUpdateDto? model, int userId);
    CategoryDto? Delete(int id,int userIdClaim);
}