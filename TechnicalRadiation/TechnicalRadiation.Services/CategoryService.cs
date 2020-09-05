using System.Collections;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
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
        
        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }
        
        public CategoryDto GetCategoryById(int id)
        {
            return _categoryRepository.GetCategoryById(id);
        }


    }
}