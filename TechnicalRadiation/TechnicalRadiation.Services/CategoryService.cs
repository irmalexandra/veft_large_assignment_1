using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class CategoryService
    {
        private CategoryRepository _categoryRepository;
        
        public CategoryService() // Constructor
        {
            _categoryRepository = new CategoryRepository();  // instance of class
        }

        private void AddLinksToCategory(HyperMediaModel c, int id)
        {
            c.Links.AddReference("self", new {href = $"/api/categories/{id})"});
            c.Links.AddReference("edit", new {href = $"/api/categories/{id})"});
            c.Links.AddReference("delete", new {href = $"/api/categories/{id})"});
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
    }
}