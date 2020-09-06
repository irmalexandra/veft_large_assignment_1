using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
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

        private AuthorDetailDto ToAuthorDetailDto(Author author)
        {
            return new AuthorDetailDto()
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                ProfileImageSource = author.ProfileImageSource
            };
        }
        
        private Author ToAuthor(AuthorInputModel author, int id)
        {
            return new Author
            {
                Id = id,
                Name = author.Name,
                Bio = author.Bio,
                ProfileImageSource = author.ProfileImgSource,
                
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

        }
        
        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var author = DataProvider.Authors.Select(a => ToAuthorDto(a));
            return author;
        }
        
        public AuthorDetailDto GetAuthorById(int id)
        {
            var author = DataProvider.Authors.FirstOrDefault(a => a.Id == id);
            return ToAuthorDetailDto(author);
        }

        public AuthorDto CreateAuthor(AuthorInputModel author)
        {
            var nextId = DataProvider.Authors.OrderByDescending(d => d.Id).FirstOrDefault().Id;
            
            if (nextId == null)
            {
                nextId = 1;
            }
            else
            {
                nextId += 1;
            }
            var entity = ToAuthor(author, nextId);
            DataProvider.Authors.Add(entity);
            return ToAuthorDto(entity);

        }




    }
}