using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            Badges = new HashSet<Badge>();
            CustomerVouchers = new HashSet<CustomerVoucher>();
            Orders = new HashSet<Order>();
        }

        public Guid VoucherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public decimal? Discount { get; set; }
        public decimal? MinimumOrderPrice { get; set; }
        public decimal? MaximumOrderPrice { get; set; }
        public Guid AuthorId { get; set; }

        public virtual User Author { get; set; }
        public virtual ICollection<Badge> Badges { get; set; }
        public virtual ICollection<CustomerVoucher> CustomerVouchers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
