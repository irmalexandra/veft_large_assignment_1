﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
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

        
        public IEnumerable<NewsItemDto> GetAllNewsItems() 
        {
            
            var newsItem = DataProvider.NewsItems.Select(r => ToNewsItemDto(r));
            return newsItem;
        }
        
        public NewsItemDetailDto GetNewsItemById(int id)
        {
            var newsItem = DataProvider.NewsItems.FirstOrDefault(n => n.Id == id);
            
            return ToNewsItemDetailDto(newsItem);
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