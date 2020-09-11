using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class CategoryService
    {
        private CategoryRepository _categoryRepository;
        private NewsItemRepository _newsItemRepository;
        
        public CategoryService() // Constructor
        {
            _categoryRepository = new CategoryRepository();  // instance of class
            _newsItemRepository = new NewsItemRepository();
        }

        private void AddLinksToCategory(HyperMediaModel category, int id)
        {
            category.Links.AddReference("self", new {href = $"/api/categories/{id})"});
            category.Links.AddReference("edit", new {href = $"/api/categories/{id})"});
            category.Links.AddReference("delete", new {href = $"/api/categories/{id})"});
        }
        
        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories().ToList();
            categories.ForEach(c =>
            {
                AddLinksToCategory(c, c.Id);
            });
            return categories;
        }
        
        public CategoryDetailDto GetCategoryById(int id)
        {
            
            var category = _categoryRepository.GetCategoryById(id);
            AddLinksToCategory(category, category.Id);
            return category;
        }

        public CategoryDto CreateCategory(CategoryInputModel category)
        {
            return _categoryRepository.CreateNewCategory(category);
        }

        public bool UpdateCategoryById(CategoryInputModel category, int id)
        {
            return _categoryRepository.UpdateCategoryById(category, id);
        }

        public bool DeleteCategoryById(in int id)
        {
            return _categoryRepository.DeleteAuthorById(id);
        }

        public NewsItemCategories CreateNewsItemCategory(int categoryId,int newsItemId)
        {
            if (_categoryRepository.GetCategoryById(categoryId) != null
                && _newsItemRepository.GetNewsItemById(newsItemId) != null
                && !_categoryRepository.CheckNewsItemCategoryRelation(categoryId, newsItemId))
            {
                return _categoryRepository.CreateNewsItemCategory(categoryId, newsItemId);
            }
            return null;
        }
    }
}