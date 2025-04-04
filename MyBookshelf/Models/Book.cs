﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBookshelf.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string PublishedDate { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public string PrintType { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string ImageLink { get; set; }
        public string Price { get; set; }
        public List<string> FormatAvailable { get; set; }
    }
}
