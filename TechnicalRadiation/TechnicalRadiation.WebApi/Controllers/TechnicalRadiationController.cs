using Microsoft.AspNetCore.Mvc;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/")]
    public class AuthorsController : Controller
    {
        [Route("")]
        [HttpGet]
        public IActionResult GetAllNews()
        {
            return Ok();
        }

        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetNewsById(int id)
        {
            return Ok("dis 1 news: " + id);
        }
        
        [Route("categories")]
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok("dis is all categories");
        }
        
        [Route("categories/{id:int}")]
        [HttpGet]
        public IActionResult GetCategorieById(int id)
        {
            return Ok($"dis is a category: {id}");
        }
        
        [Route("authors")]
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            return Ok("dis party of all authors");
        }
        
        [Route("authors/{id:int}")]
        [HttpGet]
        public IActionResult GetAuthorById(int id)
        {
            return Ok($"dis 1 lonely author: {id}");
        }
        
        [Route("authors/{id:int}/newsItems")]
        [HttpGet]
        public IActionResult GetNewsItemsByAuthorId(int id)
        {
            return Ok($"dis is lonely authors work: {id}");
        }
    }
}