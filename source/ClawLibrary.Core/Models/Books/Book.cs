using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClawLibrary.Core.Models.Authors;
using ClawLibrary.Core.Models.Categories;

namespace ClawLibrary.Core.Models.Books
{
    public class Book
    {
        public long Id { get; set; }
        public string Key { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }
        public string Language { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public long Paperback { get; set; }

        public DateTime PublishDate { get; set; }
        
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Status { get; set; }

        public Author Author { get; set; }
        public Category Category { get; set; }

        public override string ToString()
        {
            return $"Key: {Key}, Title: {Title}, Publisher: {Publisher}, Language: {Language}," +
                   $" Status: {Status}, CreatedDate: {CreatedDate}, ModifiedDate: {ModifiedDate}" +
                   $"Isbn: {Isbn}, Description: {Description}, Paperback: {Paperback}," +
                   $"PublishDate: {PublishDate}, Author: {Author}, Category: {Category}";
        }
    }
}
