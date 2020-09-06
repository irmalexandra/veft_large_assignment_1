﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;
using TechnicalRadiation.Services;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/")]
    public class TechnicalRadiationController : Controller
    {
        private NewsItemService _newsItemService;
        private CategoryService _categoryService;
        private AuthorService _authorService;


        public TechnicalRadiationController()  // Constructor
        {
            _categoryService = new CategoryService();
            _newsItemService = new NewsItemService();
            _authorService = new AuthorService();
        }
        
        [Route("")]
        [HttpGet]
        public IActionResult GetAllNews()
        {
            return Ok(_newsItemService.GetAllNewsItems());
        }

        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetNewsById(int id)
        {
            
            return Ok(_newsItemService.GetNewsItemById(id));
        }
        
        [Route("categories")]
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_categoryService.GetAllCategories());
        }
        
        [Route("categories/{id:int}")]
        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            return Ok(_categoryService.GetCategoryById(id));
        }
        
        [Route("authors")]
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            return Ok(_authorService.GetAllAuthors());
        }
        
        [Route("authors/{id:int}")]
        [HttpGet]
        public IActionResult GetAuthorById(int id)
        {
            return Ok(_authorService.GetAuthorById(id));
        }
        
        [Route("authors/{id:int}/newsItems")]
        [HttpGet]
        public IActionResult GetNewsItemsByAuthorId(int id)
        {
            return Ok(_newsItemService.GetNewsByAuthor(id));
        }
        
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!! Everything below is super authorized. !!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        [Route("")]
        [HttpPost]
        public IActionResult CreateNewsItem([FromBody] NewsItemsInputModel newsItem)
        {
            


            return Ok();
        }

        [Route("categories")]
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryInputModel category)
        {

            return Ok();
        }

        [Route("authors")]
        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorInputModel author)
        {
            return Ok();
        }

        [Route("{id:int}")]
        [HttpPut]
        public IActionResult UpdateNewsItem([FromBody] NewsItemsInputModel newsitem, int id)
        {
            return Ok();
        }
        
        [Route("authors/{id:int}")]
        [HttpPut]
        public IActionResult UpdateAuthor([FromBody] AuthorInputModel author, int id)
        {
            return Ok();
        }
        [Route("categories/{id:int}")]
        [HttpPut]
        public IActionResult UpdateCategory([FromBody] CategoryInputModel category, int id)
        {
            return Ok();
        }   
        
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult DeleteNewsItemById(int id)
        {
            return NoContent();
        }
        
        [Route("authors/{id:int}")]
        [HttpDelete]
        public IActionResult DeleteAuthorById(int id)
        {
            return NoContent();
        }
        [Route("categories/{id:int}")]
        [HttpDelete]
        public IActionResult DeleteCategoryById(int id)
        {
            return NoContent();
        }

        [Route("authors/{authorId:int}/newsItems/{newsItemId:int}")]
        [HttpPatch]
        // NewsItemAuthors connection table thing
        public IActionResult LinkAuthorToNewsItem(int authorId, int newsItemId)
        {
            return NoContent();
        }
        
        
            
            
            
    }
}
