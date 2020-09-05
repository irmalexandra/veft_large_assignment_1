using System.Collections;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
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

        public AuthorDto GetAuthorById(int id)
        {
            return _authorRepository.GetAuthorById(id);
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }
    }
}