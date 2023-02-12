using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Cart
    {
        public Guid CartId { get; set; }
        public Guid? ItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual Customer CartNavigation { get; set; }
    }
}
