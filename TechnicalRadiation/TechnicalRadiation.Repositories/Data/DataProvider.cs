using System;
using System.Collections.Generic;
using TechnicalRadiation.Models.Entities;

namespace TechnicalRadiation.Repositories.Data
{
    public static class DataProvider
    {
        private static readonly string _adminName = "TechnicalRadiationAdmin";

        public static List<Author> Authors = new List<Author>
        {
            new Author
            {
                Id = 1,
                Name = "Rikkharður",
                ProfileImageSource = "www.google.com",
                Bio = "I am a man",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Author
            {
                Id = 2,
                Name = "Emmilinn",
                ProfileImageSource = "google.com",
                Bio = "I am a man also",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Author
            {
                Id = 3,
                Name = "Lok",
                ProfileImageSource = "google.com",
                Bio = "I am a man also yes",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Author
            {
                Id = 4,
                Name = "i",
                ProfileImageSource = "google.com",
                Bio = "I am a man also",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            }

        };

        public static List<Category> Categories = new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "Pc",
                Slug = "pc",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Category
            {
                Id = 2,
                Name = "Consoles",
                Slug = "consoles",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            },
            new Category
            {
                Id = 3,
                Name = "Cyborgs",
                Slug = "cyborgs",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Today,
                ModifiedDate = DateTime.Now
            }
        };

        public static List<NewsItem> NewsItems = new List<NewsItem>
        {
            new NewsItem
            {
                Id = 1,
                Title = "When the sun hit the moon",
                ImageSource = "4chan.net",
                ShortDescription = "Shit was wild",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishedDate = DateTime.Now,
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new NewsItem
            {
                Id = 2,
                Title = "When the moon hit the sun",
                ImageSource = "4chan.net",
                ShortDescription = "Shit was wild",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishedDate = DateTime.Now,
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new NewsItem
            {
                Id = 3,
                Title = "Java vs C++",
                ImageSource = "4chan.net",
                ShortDescription = "Practically 0 difference to be honest",
                LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris fringilla justo sed laoreet gravida.",
                PublishedDate = DateTime.MinValue,
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            }
        };

        public static List<NewsItemAuthors> NewsItemAuthors = new List<NewsItemAuthors>
        {
            new NewsItemAuthors
            {
                AuthorId = 1,
                NewsItemId = 1
            },
            new NewsItemAuthors
            {
                AuthorId = 3,
                NewsItemId = 2
            }
            ,
            new NewsItemAuthors
            {
                AuthorId = 3,
                NewsItemId = 3
            }
        };

        public static List<NewsItemCategories> NewsItemCategories = new List<NewsItemCategories>
        {
            new NewsItemCategories()
            {
                CategoryId = 1,
                NewsItemId = 1
            },
            new NewsItemCategories()
            {
                CategoryId = 3,
                NewsItemId = 2
            }
        };

    }
}