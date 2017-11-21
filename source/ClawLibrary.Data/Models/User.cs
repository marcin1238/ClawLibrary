using System;
using System.Collections.Generic;

namespace ClawLibrary.Data.Models
{
    public class User
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public Guid Key { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordResetKey { get; set; }
        public DateTimeOffset? PasswordResetKeyCreatedDate { get; set; }
        public long? ImageFileId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Status { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual File ImageFile { get; set; }
    }
}
