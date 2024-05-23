using App.Dtos;
using App.IServices;
using AutoMapper;
using DataAccess.IRepositories;
using DataAccess.Models;

namespace App.Services
{
    public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        : ICategoryService
    {

        public List<CategoryDto> FindAll(int userIdClaim)
        {
            var categories = categoryRepository.FindAll(userIdClaim);
            return mapper.Map<List<CategoryDto>>(categories);
        }

        public List<CategoryDto> FindAllByContent(string text, int userIdClaim)
        {
            var categories = categoryRepository.FindAllByContent(text,userIdClaim);
            return mapper.Map<List<CategoryDto>>(categories);
        }

        public CategoryDto? GetById(int id,int userIdClaim)
        {
            var category = categoryRepository.GetById(id,userIdClaim);
            return mapper.Map<CategoryDto>(category);
        }

        public CategoryDto? Create(CategoryCreateDto? model, int userId)
        {
            var newCategory = mapper.Map<Category>(model);
            newCategory.UserId = userId;
            var createdCategory = categoryRepository.Create(mapper.Map<Category>(newCategory ?? throw new ArgumentNullException(nameof(model))));
            return mapper.Map<CategoryDto>(createdCategory);
        }

        public CategoryDto? Update(CategoryUpdateDto? model, int userId)
        {
            var updateCategory = mapper.Map<Category>(model);
            updateCategory.UserId = userId;
            var update = categoryRepository.Update(mapper.Map<Category>(updateCategory ?? throw new ArgumentNullException(nameof(model))));
            return mapper.Map<CategoryDto>(update);
        }

        public CategoryDto Delete(int id,int userIdClaim)
        {
            var deleted = categoryRepository.Delete(id,userIdClaim);
            return mapper.Map<CategoryDto>(deleted);
        }
    }
}
