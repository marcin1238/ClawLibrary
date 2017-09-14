using System;
using System.Collections.Generic;

namespace ClawLibrary.Data.Models
{
    public class Category
    {
        public Category()
        {
            Book = new HashSet<Book>();
        }

        public long Id { get; set; }
        public Guid Key { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
