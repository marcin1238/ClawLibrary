using System;
using System.Collections.Generic;

namespace ClawLibrary.Data.Models
{
    public class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        public long Id { get; set; }
        public Guid Key { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public long? ImageFileId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Book> Book { get; set; }
        public virtual File ImageFile { get; set; }
    }
}
