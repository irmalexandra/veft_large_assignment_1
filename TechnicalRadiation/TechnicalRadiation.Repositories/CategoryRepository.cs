using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Data;

namespace TechnicalRadiation.Repositories
{
    public class CategoryRepository
    {
        private CategoryDto ToCategoryDto (Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug
            };
        }
        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var category = DataProvider.Categories.Select(c => ToCategoryDto(c));
            return category;
        }
        public CategoryDto GetCategoryById(int id)
        {
            var category = DataProvider.Categories.FirstOrDefault(n => n.Id == id);
            
            return ToCategoryDto(category);
        }


    }
}