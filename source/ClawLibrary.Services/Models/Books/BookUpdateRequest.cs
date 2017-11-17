using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClawLibrary.Core.Enums;

namespace ClawLibrary.Services.Models.Books
{
    public class BookUpdateRequest
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
        public long Paperback { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorKey { get; set; }
        public string CategoryKey { get; set; }


        public override string ToString()
        {
            return $"Title: {Title}, Publisher: {Publisher}, Language: {Language}, CategoryKey: {CategoryKey}, AuthorKey: {AuthorKey}" +
                   $"Isbn: {Isbn}, Description: {Description}, Quantity: {Quantity}, Paperback: {Paperback}, PublishDate: {PublishDate}";
        }
    }
}
