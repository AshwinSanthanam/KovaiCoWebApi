using System;

namespace KC.Base.Models
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
    }
}
