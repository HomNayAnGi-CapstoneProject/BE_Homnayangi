using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        public Guid CartId { get; set; }
        public Guid? CustomerId { get; set; }
        public int? QuantityOfItem { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
