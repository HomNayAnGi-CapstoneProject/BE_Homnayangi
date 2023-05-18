using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class CustomerBadge
    {
        public Guid CustomerId { get; set; }
        public Guid BadgeId { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Badge Badge { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
