﻿using System;
using System.Collections.Generic;

namespace BusinessNewsApp.Models
{
    public class NewsArticle
    {
        public string SourceName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

    public class Source
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }

    public class Article
    {
        public Source? Source { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string? Content { get; set; }
    }

    public class NewsApiResponse
    {
        public string? Status { get; set; }
        public int TotalResults { get; set; }
        public List<Article>? Articles { get; set; }
    }
}
