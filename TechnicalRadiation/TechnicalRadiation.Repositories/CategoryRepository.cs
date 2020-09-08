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
        private static readonly string _adminName = "TechnicalRadiationAdmin";
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
                Slug = category.Name.ToLower().Replace(" ", "-"),
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
            var nextId = DataProvider.Categories.Count()+1;

            var entity = toCategoryItem(categoryitem, nextId);
            DataProvider.Categories.Add(entity);
            return ToCategoryDto(entity);

        }

        public bool UpdateCategoryById(CategoryInputModel category, int id)
        {
            Category oldCategory = DataProvider.Categories.FirstOrDefault(author => author.Id == id);
            if (oldCategory == null)
            {
                return false;
            }

            oldCategory.Name = category.Name;
            oldCategory.Slug = oldCategory.Name.ToLower().Replace(" ", "-");
            oldCategory.ModifiedBy = _adminName;
            oldCategory.ModifiedDate = DateTime.Now;
            return true;
        }

        public bool DeleteAuthorById(int id)
        {
            return DataProvider.Categories.Remove(DataProvider.Categories.FirstOrDefault(news => news.Id == id));
        }
        
        public IEnumerable<NewsItemCategories> GetCategoryByNewsItemId(int newsItemId)
        {
            return DataProvider.NewsItemCategories.Where(n => n.NewsItemId == newsItemId);
        }
    }
}