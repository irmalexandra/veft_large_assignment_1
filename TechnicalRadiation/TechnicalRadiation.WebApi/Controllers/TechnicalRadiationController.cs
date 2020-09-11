using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services;
using TechnicalRadiation.WebApi.Attributes;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/")]
    public class TechnicalRadiationController : Controller
    {
        private NewsItemService _newsItemService;
        private CategoryService _categoryService;
        private AuthorService _authorService;


        public TechnicalRadiationController() // Constructor
        {
            _categoryService = new CategoryService();
            _newsItemService = new NewsItemService();
            _authorService = new AuthorService();
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAllNews()
        {
            var pageSize = 25;
            var pageNumber = 0;
            if (HttpContext.Request.QueryString.HasValue)
            {
                var sizeString = HttpContext.Request.Query["pageSize"];
                var numberString = HttpContext.Request.Query["pageNumber"];
                if (!int.TryParse(sizeString, out pageSize) || !int.TryParse(numberString, out pageNumber))
                {
                    return BadRequest("Invalid parameters");
                }
            }

            return Ok(_newsItemService.GetAllNewsItems(pageSize, pageNumber));
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

        [Route("", Name = "CreateNewsItem")]
        [Authorization]
        [HttpPost]
        public IActionResult CreateNewsItem([FromBody] NewsItemsInputModel newsItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The News item model was badly constructed, feel bad");
            }

            var newsItemDto = _newsItemService.CreateNewsItem(newsItem);

            return CreatedAtRoute("CreateNewsItem", new {id = newsItemDto.Id}, null);
        }

        [Route("categories", Name = "CreateCategory")]
        [Authorization]
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryInputModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The category was badly constructed");
            }

            var categoryDto = _categoryService.CreateCategory(category);
            return CreatedAtRoute("CreateCategory", new {id = categoryDto.Id}, null);
        }

        [Route("authors", Name = "CreateAuthor")]
        [Authorization]
        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorInputModel author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The author was badly constructed");
            }

            var authorDto = _authorService.CreateAuthor(author);
            return CreatedAtRoute("CreateAuthor", new {id = authorDto.Id}, null);
        }

        [Route("{id:int}")]
        [Authorization]
        [HttpPut]
        public IActionResult UpdateNewsItem([FromBody] NewsItemsInputModel newsitem, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = _newsItemService.UpdateNewsItemById(newsitem, id);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        [Route("authors/{id:int}")]
        [Authorization]
        [HttpPut]
        public IActionResult UpdateAuthor([FromBody] AuthorInputModel author, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = _authorService.UpdateAuthorById(author, id);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        [Route("categories/{id:int}")]
        [Authorization]
        [HttpPut]
        public IActionResult UpdateCategory([FromBody] CategoryInputModel category, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var success = _categoryService.UpdateCategoryById(category, id);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }
        
        [Route("{id:int}")]
        [Authorization]
        [HttpDelete]
        public IActionResult DeleteNewsItemById(int id)
        {
            bool success = _newsItemService.DeleteNewsItemById(id);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        [Route("authors/{id:int}")]
        [Authorization]
        [HttpDelete]
        public IActionResult DeleteAuthorById(int id)
        {
            bool success = _authorService.DeleteAuthorById(id);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        [Route("categories/{id:int}")]
        [Authorization]
        [HttpDelete]
        public IActionResult DeleteCategoryById(int id)
        {
            bool success = _categoryService.DeleteCategoryById(id);
            if (success)
            {
                return NoContent();
            }

            return NotFound();
        }

        [Route("authors/{authorId:int}/newsItems/{newsItemId:int}", Name = "CreateNewsItemAuthor")]
        [Authorization]
        [HttpPost]
        // NewsItemAuthors connection table
        public IActionResult LinkAuthorToNewsItem(int authorId, int newsItemId)
        {
            var newsItemAuthor = _authorService.CreateNewsItemAuthor(authorId, newsItemId);
            if (newsItemAuthor != null)
            {
                return CreatedAtRoute("CreateNewsItemAuthor", null);
            }

            return BadRequest();
        }
    }
}