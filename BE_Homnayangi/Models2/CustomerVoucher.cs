using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class CustomerVoucher
    {
        public Guid VoucherId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Quantity { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
