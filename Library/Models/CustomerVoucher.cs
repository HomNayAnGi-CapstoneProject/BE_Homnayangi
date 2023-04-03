using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class CustomerVoucher
    {
        public Guid CustomerVoucherId { get; set; }
        public Guid VoucherId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
