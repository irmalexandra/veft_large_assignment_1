﻿using System;

namespace TechnicalRadiation.Models.Dtos
{
    public class NewsItemDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime PublishedDate { get; set; }

    }
}