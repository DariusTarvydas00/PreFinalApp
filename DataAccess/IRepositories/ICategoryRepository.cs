using DataAccess.Models;

namespace DataAccess.IRepositories;

public interface ICategoryRepository
{
    List<Category?> FindAll(int userId);
    List<Category?> FindAllByContent(string text,int userId);
    Category? GetById(int id,int userId);
    Category? Create(Category entity);
    Category? Update(Category entity);
    Category Delete(int id,int userId);
}