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
        public Envelope<NewsItemDto> GetAllNewsItems(int pageSize, int pageNumber) 
        {
            var news = DataProvider.NewsItems.Select(n => ToNewsItemDto(n));
            var collection = new Envelope<NewsItemDto>(pageNumber, pageSize, news);
            return collection;
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