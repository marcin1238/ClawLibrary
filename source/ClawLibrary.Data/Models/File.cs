using System;
using System.Collections.Generic;

namespace ClawLibrary.Data.Models
{
    public class File
    {
        public File()
        {
            Author = new HashSet<Author>();
            Book = new HashSet<Book>();
            User = new HashSet<User>();
        }

        public long Id { get; set; }
        public Guid Key { get; set; }
        public string FileName { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Author> Author { get; set; }
        public virtual ICollection<Book> Book { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
