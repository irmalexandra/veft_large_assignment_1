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
        
        public AuthorService() // Constructor
        {
            _authorRepository = new AuthorRepository();  // instance of class
            
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


    }
}