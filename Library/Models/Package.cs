using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Package
    {
        public Package()
        {
            OrderDetails = new HashSet<OrderDetail>();
            PackageDetails = new HashSet<PackageDetail>();
        }

        public Guid PackageId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal? PackagePrice { get; set; }
        public bool? IsCooked { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? Size { get; set; }
        public Guid? BlogId { get; set; }
        public int? Status { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<PackageDetail> PackageDetails { get; set; }
    }
}
