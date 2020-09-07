using System.Collections;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;
using TechnicalRadiation.Models.Extensions;

namespace TechnicalRadiation.Services
{
    public class NewsItemService
    {
        private NewsItemRepository _newsItemRepository;
        
        public NewsItemService() // Constructor
        {
            _newsItemRepository = new NewsItemRepository();
            
        }

        public IEnumerable<NewsItemDto> GetAllNewsItems(int pageSize, int pageNumber)
        {
            var listStart = 0;
            if (pageNumber > 1) {listStart = pageSize * pageNumber - 1;}
            var listEnd = pageSize + listStart;
            return _newsItemRepository.GetAllNewsItems(listStart, listEnd);
        }
        
        public NewsItemDetailDto GetNewsItemById(int id)
        {
            return _newsItemRepository.GetNewsItemById(id);
        }

        public IEnumerable<NewsItemDto> GetNewsByAuthor(int id)
        {
            return _newsItemRepository.GetNewsItemsByAuthorId(id);
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