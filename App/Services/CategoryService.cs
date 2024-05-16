using App.IServices;
using DataAccess.IRepositories;
using DataAccess.Models;

namespace App.Services
{
    public class CategoryService(ICategoryRepository categoryRepository)
        : ICategoryService
    {

        public List<Category?> FindAll(int userIdClaim)
        {
            return categoryRepository.FindAll(userIdClaim);
        }

        public List<Category?> FindAllByContent(string text,int userIdClaim)
        {
                return categoryRepository.FindAllByContent(text,userIdClaim);
        }

        public Category? GetById(int id,int userIdClaim)
        {
            return categoryRepository.GetById(id,userIdClaim);
        }

        public Category? Create(Category? model)
        {
            return categoryRepository.Create(model ?? throw new ArgumentNullException(nameof(model)));
        }

        public Category? Update(Category? model)
        {
            return categoryRepository.Update(model ?? throw new ArgumentNullException(nameof(model)));
        }

        public Category Delete(int id,int userIdClaim)
        {
            return categoryRepository.Delete(id,userIdClaim);
        }
    }
}
