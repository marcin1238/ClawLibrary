using System;

namespace ClawLibrary.Data.Models
{
    public class Order
    {
        public long Id { get; set; }
        public Guid Key { get; set; }
        public long BookId { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Status { get; set; }

        public virtual Book Book { get; set; }
    }
}
