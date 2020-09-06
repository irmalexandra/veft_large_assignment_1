using System.Collections;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class AuthorService
    {
        private AuthorRepository _authorRepository;
        private NewsItemRepository _newsItemRepository;
        
        public AuthorService() // Constructor
        {
            _authorRepository = new AuthorRepository();  // instance of class
            _newsItemRepository = new NewsItemRepository();
        }
        
        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }
        
        public AuthorDetailDto GetAuthorById(int id)
        {
            return _authorRepository.GetAuthorById(id);
        }

        public AuthorDto CreateAuthor(AuthorInputModel author)
        {
            return _authorRepository.CreateAuthor(author);
        }

        public NewsItemAuthors CreateNewsItemAuthor(int authorId, int newsItemId)
        {
            if (_authorRepository.GetAuthorById(authorId) != null
                && _newsItemRepository.GetNewsItemById(newsItemId) != null
                && !_authorRepository.CheckNewsItemAuthorRelation(newsItemId))
            {
                return _authorRepository.CreateNewsItemAuthor(authorId, newsItemId);
            }
            return null;
        }
    }
}