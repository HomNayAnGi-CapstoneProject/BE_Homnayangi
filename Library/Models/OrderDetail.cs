using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class OrderDetail
    {
        public Guid OrderDetailId { get; set; }
        public Guid OrderId { get; set; }
        public int? Quantity { get; set; }
        public Guid? PackageId { get; set; }
        public decimal? Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual Package Package { get; set; }
    }
}
