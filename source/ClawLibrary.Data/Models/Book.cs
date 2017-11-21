using System;
using System.Collections.Generic;

namespace ClawLibrary.Data.Models
{
    public class Book
    {
        public long Id { get; set; }
        public Guid Key { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public long Paperback { get; set; }
        public DateTime PublishDate { get; set; }
        public long AuthorId { get; set; }
        public long? ImageFileId { get; set; }
        public long CategoryId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Status { get; set; }
        
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual File ImageFile { get; set; }
    }
}
