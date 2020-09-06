using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Data;

namespace TechnicalRadiation.Repositories
{
    public class NewsItemRepository
    {
        
        private NewsItemDto ToNewsItemDto(NewsItem newsitem)
        {
            return new NewsItemDto
            {
                Id = newsitem.Id,
                ImageSource = newsitem.ImageSource,
                ShortDescription = newsitem.ShortDescription,
                Title = newsitem.Title,
                
            };
        }
        
        private NewsItemDetailDto ToNewsItemDetailDto(NewsItem newsitem)
        {
            return new NewsItemDetailDto
            {
                Id = newsitem.Id,
                ImageSource = newsitem.ImageSource,
                ShortDescription = newsitem.ShortDescription,
                Title = newsitem.Title,
                LongDescription = newsitem.LongDescription,
                PublishedDate = newsitem.PublishedDate,
                
            };
            
        }

        private NewsItem ToNewsItem(NewsItemsInputModel newsItem, int id)
        {
            return new NewsItem
            {
                Id = id,
                Title = newsItem.Title,
                ImageSource = newsItem.ImgSource,
                ShortDescription = newsItem.ShortDescription,
                LongDescription = newsItem.LongDescription,
                PublishedDate = newsItem.PublishDate,
                
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

        }

        
        public IEnumerable<NewsItemDto> GetAllNewsItems() 
        {
            
            var newsItems = DataProvider.NewsItems.Select(r => ToNewsItemDto(r));
            return newsItems;
        }
        
        public NewsItemDetailDto GetNewsItemById(int id)
        {
            var newsItem = DataProvider.NewsItems.FirstOrDefault(n => n.Id == id);
            
            return ToNewsItemDetailDto(newsItem);
        }

        public IEnumerable<NewsItemDto> GetNewsItemsByAuthorId(int id)
        {
            
            var thing = from News in DataProvider.NewsItems
                join AuthorNews in DataProvider.NewsItemAuthors on News.Id equals AuthorNews.NewsItemId
                where AuthorNews.AuthorId == id
                select ToNewsItemDto(News);
            return thing;

        }

        public NewsItemDto CreateNewsItem(NewsItemsInputModel newsItem)
        {
            var nextId = DataProvider.NewsItems.OrderByDescending(n => n.Id)
                .FirstOrDefault().Id;
            if (nextId == null)
            {
                nextId = 1;
            }
            else
            {
                nextId += 1;
            }

            var entity = ToNewsItem(newsItem, nextId);
            DataProvider.NewsItems.Add(entity);
            return ToNewsItemDto(entity);

        }
        
    }
}
/*
public class OwnerRepository
{
    private IMapper _mapper;

    public OwnerRepository(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IEnumerable<OwnerDto> GetOwnersByRentalId(int rentalId)
    {
        return _mapper.Map<IEnumerable<OwnerDto>>(DataProvider.Owners.Where(o => o.RentalId == rentalId));
    }

    public OwnerDto GetOwnerByRentalId(int rentalId, int ownerId)
    {
        var owner = DataProvider.Owners.FirstOrDefault(o => o.Id == ownerId && o.RentalId == rentalId);
        if (owner == null) { throw new Exception("Owner not found"); }
        return _mapper.Map<OwnerDto>(owner);
    }
}
*/