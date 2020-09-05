﻿using System.Collections;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class NewsItemService
    {
        private NewsItemRepository _newsItemRepository;

        public NewsItemService() // Constructor
        {
            _newsItemRepository = new NewsItemRepository();
            
        }
        
        public IEnumerable<NewsItemDto> GetAllNewsItems()
        {
            return _newsItemRepository.GetAllNewsItems();
        }
        
        public NewsItemDetailDto GetNewsItemById(int id)
        {
            return _newsItemRepository.GetNewsItemById(id);
        }


    }
}

/*
public class OwnerService
{
    private OwnerRepository _ownerRepository;
        
    public OwnerService(IMapper mapper)
    {
        _ownerRepository = new OwnerRepository(mapper);
    }

    public IEnumerable<OwnerDto> GetOwnersByRentalId(int rentalId)
    {
        return _ownerRepository.GetOwnersByRentalId(rentalId);
    }
    public OwnerDto GetOwnerByRentalId(int rentalId, int ownerId)
    {
        return _ownerRepository.GetOwnerByRentalId(rentalId, ownerId);
    }
}*/