using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Badge
    {
        public Badge()
        {
            BadgeConditions = new HashSet<BadgeCondition>();
            CustomerBadges = new HashSet<CustomerBadge>();
        }

        public Guid BadgeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ImageUrl { get; set; }
        public int? Status { get; set; }
        public Guid? VoucherId { get; set; }

        public virtual Voucher Voucher { get; set; }
        public virtual ICollection<BadgeCondition> BadgeConditions { get; set; }
        public virtual ICollection<CustomerBadge> CustomerBadges { get; set; }
    }
}
