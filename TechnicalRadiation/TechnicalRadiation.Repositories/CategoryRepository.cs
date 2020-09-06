using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
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
        private Category toCategoryItem(CategoryInputModel category, int id)
        {
            return new Category
            {
                Id = id,
                Name = category.Name,
                Slug = category.Name.ToLower(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
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
        public CategoryDto CreateNewCategory(CategoryInputModel categoryitem)
        {
            var nextId = DataProvider.Categories.OrderByDescending(n => n.Id)
                .FirstOrDefault().Id;
            if (nextId == null)
            {
                nextId = 1;
            }
            else
            {
                nextId += 1;
            }

            var entity = toCategoryItem(categoryitem, nextId);
            DataProvider.Categories.Add(entity);
            return ToCategoryDto(entity);

        }

    }
}