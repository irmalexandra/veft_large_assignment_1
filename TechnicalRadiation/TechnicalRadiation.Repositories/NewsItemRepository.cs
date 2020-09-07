using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Data;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Extensions;

namespace TechnicalRadiation.Repositories
{
    public class NewsItemRepository
    {
        private static readonly string _adminName = "TechnicalRadiationAdmin";
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
                PublishDate = newsitem.PublishDate,
                
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
                PublishDate = newsItem.PublishDate,
                
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            }; 

        }
        private NewsItemDto CreateLinksForNewsItem(NewsItemDto newsItem)
        {

            var query = from authorids in DataProvider.NewsItemAuthors
                where authorids.NewsItemId == newsItem.Id
                select authorids.NewsItemId;
            
            newsItem.Links.AddReference("self", new { href = $"api/{newsItem.Id})" });
            newsItem.Links.AddReference("edit", new { href = $"api/{newsItem.Id})" });
            newsItem.Links.AddReference("delete", new { href = $"api/{newsItem.Id})" });
            //newsItem.Links.AddListReference("authors", new { $"/api/authors/{query}"});
            
            return newsItem;
        }

        public IEnumerable<NewsItemDto> GetAllNewsItems(int listStart, int listEnd) 
        {
            var query = 
                from news in DataProvider.NewsItems.Skip(listStart).Take(listEnd)
                orderby news.PublishDate
                select CreateLinksForNewsItem(ToNewsItemDto(news));
            return query;
        }
        
        public NewsItemDetailDto GetNewsItemById(int id)
        {
            var newsItem = DataProvider.NewsItems.FirstOrDefault(n => n.Id == id);
            return ToNewsItemDetailDto(newsItem);
        }

        public IEnumerable<NewsItemDto> GetNewsItemsByAuthorId(int id)
        {
            var query = from news in DataProvider.NewsItems
                join authorNews in DataProvider.NewsItemAuthors on news.Id equals authorNews.NewsItemId
                where authorNews.AuthorId == id
                select ToNewsItemDto(news);
            return query;
        }

        public NewsItemDto CreateNewsItem(NewsItemsInputModel newsItem)
        {
            var nextId = DataProvider.NewsItems.Count()+1;
            
            var entity = ToNewsItem(newsItem, nextId);
            DataProvider.NewsItems.Add(entity);
            return ToNewsItemDto(entity);
        }

        public bool UpdateNewsItemById(NewsItemsInputModel newsItem, int id)
        {
            NewsItem oldNewsItem = DataProvider.NewsItems.FirstOrDefault(news => news.Id == id);
            if (oldNewsItem == null)
            {
                return false;
            }

            oldNewsItem.Title = newsItem.Title;
            oldNewsItem.ImageSource = newsItem.ImgSource;
            oldNewsItem.LongDescription = newsItem.LongDescription;
            oldNewsItem.ShortDescription = newsItem.ShortDescription;
            oldNewsItem.PublishDate = newsItem.PublishDate;
            oldNewsItem.ModifiedBy = _adminName;
            oldNewsItem.ModifiedDate = DateTime.Now;

            return true;
        }

        public bool DeleteNewsItemById(int id)
        {
            return DataProvider.NewsItems.Remove(DataProvider.NewsItems.FirstOrDefault(news => news.Id == id));
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