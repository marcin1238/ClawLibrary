using System;
using ClawLibrary.Core.Enums;
using ClawLibrary.Services.Models.Authors;
using ClawLibrary.Services.Models.Categories;

namespace ClawLibrary.Services.Models.Books
{
    public class BookResponse
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public long Paperback { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public Status Status { get; set; }

        public AuthorResponse Author { get; set; }
        public CategoryResponse Category { get; set; }

        public override string ToString()
        {
            return $"Key: {Key}, Title: {Title}, Publisher: {Publisher}" +
                   $"Language {Language}, Isbn: {Isbn}, Description: {Description}, Paperback: {Paperback}" +
                   $"PublishDate {PublishDate}, CreatedDate: {CreatedDate}, ModifiedDate: {ModifiedDate}, Status: {Status}, Author: {Author}, Category: {Category}";
        }
    }
}