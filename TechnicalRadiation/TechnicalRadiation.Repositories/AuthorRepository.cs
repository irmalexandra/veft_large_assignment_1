using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Data;

namespace TechnicalRadiation.Repositories
{
    public class AuthorRepository
    {
        
        private AuthorDto ToAuthorDto (Author author)
        {
            return new AuthorDto()
            {
                Id = author.Id,
                Name = author.Name,
            };
        }
        
        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var author = DataProvider.Authors.Select(a => ToAuthorDto(a));
            return author;
        }
        
        public AuthorDto GetAuthorById(int id)
        {
            var author = DataProvider.Authors.FirstOrDefault(a => a.Id == id);
            
            return ToAuthorDto(author);
        }




    }
}