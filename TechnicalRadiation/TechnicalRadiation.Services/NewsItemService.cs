using System.Collections;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
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

        public IEnumerable<NewsItemDto> GetNewsByAuthor(int id)
        {
            return _newsItemRepository.GetNewsItemsByAuthorId(id);
        }

        public NewsItemDto CreateNewsItem(NewsItemsInputModel newsItem)
        {
            return _newsItemRepository.CreateNewsItem(newsItem);
        }

        public void UpdateNewsItemById(NewsItemsInputModel newsitem, int id)
        {
            _newsItemRepository.UpdateNewsItemById(newsitem, id);
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