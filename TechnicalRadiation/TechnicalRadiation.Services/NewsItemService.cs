using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;
using TechnicalRadiation.Models.Extensions;
using System.Linq;
using TechnicalRadiation.Models;


namespace TechnicalRadiation.Services
{
    public class NewsItemService
    {
        private NewsItemRepository _newsItemRepository;
        private AuthorRepository _authorRepository;
        private CategoryRepository _categoryRepository;
        
        public NewsItemService() // Constructor
        {
            _newsItemRepository = new NewsItemRepository();
            _authorRepository = new AuthorRepository();
            _categoryRepository = new CategoryRepository();
            
        }

        private void AddLinksToNewsItems(HyperMediaModel n, int id )
        {
   
                n.Links.AddReference("self", new {href = $"/api/{id}"});
                n.Links.AddReference("edit", new {href = $"/api/{id}"});
                n.Links.AddReference("delete", new {href = $"/api/{id}"});
                n.Links.AddListReference("authors",
                    _authorRepository.GetAuthorsByNewsItemId(id).Select(a => 
                        new {href = $"/api/authors/{id}"}));
                n.Links.AddListReference("categories", _categoryRepository.GetCategoryByNewsItemId(id).Select(c => 
                    new {href = $"/api/categories/{id}"}));
            }


        public IEnumerable<NewsItemDto> GetAllNewsItems(int pageSize, int pageNumber)
        {
            var listStart = 0;
            if (pageNumber > 1) {listStart = pageSize * pageNumber - 1;}
            var listEnd = pageSize + listStart;

            var news = _newsItemRepository.GetAllNewsItems(listStart, listEnd).ToList();

            news.ForEach(n =>
            {
                AddLinksToNewsItems(n, n.Id);
            });
            return news ;
        }
        
        public NewsItemDetailDto GetNewsItemById(int id)
        {
            var news = _newsItemRepository.GetNewsItemById(id);
            AddLinksToNewsItems(news, news.Id);
            return news;
        }

        public IEnumerable<NewsItemDto> GetNewsByAuthor(int id)
        {
            var news = _newsItemRepository.GetNewsItemsByAuthorId(id).ToList();
            news.ForEach(n =>
            {
                AddLinksToNewsItems(n, n.Id);
            });
            return news;
        }

        public NewsItemDto CreateNewsItem(NewsItemsInputModel newsItem)
        {
            return _newsItemRepository.CreateNewsItem(newsItem);
        }

        public bool UpdateNewsItemById(NewsItemsInputModel newsitem, int id)
        {
            return _newsItemRepository.UpdateNewsItemById(newsitem, id);
        }

        public bool DeleteNewsItemById(in int id)
        {
            return _newsItemRepository.DeleteNewsItemById(id);
        }
    }
}