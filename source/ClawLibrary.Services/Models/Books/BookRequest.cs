using System;

namespace ClawLibrary.Services.Models.Books
{
    public class BookRequest
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public long Paperback { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorKey { get; set; }
        public string CategoryKey { get; set; }


        public override string ToString()
        {
            return $"Title: {Title}, Publisher: {Publisher}, Language: {Language}, CategoryKey: {CategoryKey}, AuthorKey: {AuthorKey}" +
                   $"Isbn: {Isbn}, Description: {Description}, Paperback: {Paperback}, PublishDate: {PublishDate}";
        }
    }
}