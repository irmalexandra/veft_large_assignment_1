using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.Extensions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class AuthorService
    {
        private AuthorRepository _authorRepository;
        private NewsItemRepository _newsItemRepository;

        private void AddLinksToAuthorDto(HyperMediaModel a, int Id)
        {
            a.Links.AddReference("self" , new {href = $"/api/authors/{Id}"});
            a.Links.AddReference("edit" , new {href = $"/api/authors/{Id}"});
            a.Links.AddReference("delete" , new {href = $"/api/authors/{Id}"});
            a.Links.AddReference("newsItems" , new {href = $"/api/authors/{Id}/newsItems"});
            a.Links.AddListReference("newsItemsDetailed",
                _newsItemRepository.GetNewsItemsByAuthorId(Id).Select(n => new {href = $"/api/{n.Id}"}));
        }

        public AuthorService() // Constructor
        {
            
            _authorRepository = new AuthorRepository();  // instance of class
            _newsItemRepository = new NewsItemRepository();
        }
        
        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var authors = _authorRepository.GetAllAuthors().ToList();
            authors.ForEach(author =>
            {
                AddLinksToAuthorDto(author, author.Id);
            });
            return authors;
        }
        
        public AuthorDetailDto GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            AddLinksToAuthorDto(author, author.Id);
            return author;
        }

        public AuthorDto CreateAuthor(AuthorInputModel author)
        {
            return _authorRepository.CreateAuthor(author);
        }

        public NewsItemAuthors CreateNewsItemAuthor(int authorId, int newsItemId)
        {
            if (_authorRepository.GetAuthorById(authorId) != null
                && _newsItemRepository.GetNewsItemById(newsItemId) != null
                && !_authorRepository.CheckNewsItemAuthorRelation(authorId, newsItemId))
            {
                return _authorRepository.CreateNewsItemAuthor(authorId, newsItemId);
            }
            return null;
        }

        public bool UpdateAuthorById(AuthorInputModel author, int id)
        {
            return _authorRepository.UpdateAuthorById(author, id);
        }

        public bool DeleteAuthorById(int id)
        {
            return _authorRepository.DeleteAuthorById(id);
        }


    }
}