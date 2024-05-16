using DataAccess.Models;

namespace App.IServices;

public interface ICategoryService
{
    List<Category?> FindAll(int userIdClaim);
    List<Category?> FindAllByContent(string text,int userIdClaim);
    Category? GetById(int id,int userIdClaim);
    Category? Create(Category? model);
    Category? Update(Category? model);
    Category? Delete(int id,int userIdClaim);
}