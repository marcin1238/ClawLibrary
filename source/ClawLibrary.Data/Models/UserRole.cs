using System;

namespace ClawLibrary.Data.Models
{
    public class UserRole
    {
        public long Id { get; set; }
        public Guid Key { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Status { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
