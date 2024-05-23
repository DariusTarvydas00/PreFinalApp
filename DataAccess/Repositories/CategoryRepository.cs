using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class CategoryRepository(MainDbContext mainDbContext) : ICategoryRepository
{
    public List<Category?> FindAll(int userId)
    {
        return mainDbContext.Categories.Include(category => category.Notes).Where(c => c != null && c.UserId == userId).ToList();
    }

    public List<Category?> FindAllByContent(string text,int userId)
    {
        return mainDbContext.Categories.Include(category => category.Notes).Where(category => category != null && category.User != null && category.Name.Contains(text) && category.UserId == userId).ToList();
    }

    public Category? GetById(int id,int userId)
    {
        return mainDbContext.Categories.FirstOrDefault(category => category != null && category.Id == id && category.UserId == userId);
    }

    public Category? Create(Category entity)
    {
        var category = mainDbContext.Categories.Add(entity);
        mainDbContext.SaveChanges();
        return category.Entity;
    }

    public Category? Update(Category entity)
    {
        var cat = mainDbContext.Categories.FirstOrDefault(category => category != null && category.Id == entity.Id && category.UserId == entity.UserId);
        if (cat != null) cat.Name = entity.Name;
        mainDbContext.SaveChanges();
        return cat;
    }

    public Category Delete(int id,int userId)
    {
        var cat = mainDbContext.Categories.FirstOrDefault(category => category != null && category.UserId == userId && category.Id == id);
        if (cat == null)
        {
            throw new InvalidDataException("Category does not exist with such Id");
        }
        mainDbContext.Categories.Remove(cat);
        mainDbContext.SaveChanges();
        return cat;
    }
}