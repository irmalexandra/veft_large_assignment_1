using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;
using TechnicalRadiation.Models.Extensions;
using System.Linq;
using Exterminator.Models.Exceptions;
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
                        new {href = $"/api/authors/{a.AuthorId}"}));
                n.Links.AddListReference("categories", _categoryRepository.GetCategoryByNewsItemId(id).Select(c => 
                    new {href = $"/api/categories/{c.CategoryId}"}));
            }


        public Envelope<NewsItemDto> GetAllNewsItems(int pageSize, int pageNumber)
        {
            var news = _newsItemRepository.GetAllNewsItems(pageSize, pageNumber);

            foreach (var newsItem in news.Items) {AddLinksToNewsItems(newsItem, newsItem.Id);}
            return news ;
        }
        
        public NewsItemDetailDto GetNewsItemById(int id)
        {
            var news = _newsItemRepository.GetNewsItemById(id);
            if (news == null)
            {
                throw new ResourceNotFoundException($"News item with id {id} was not found, my guy. ");
            }
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