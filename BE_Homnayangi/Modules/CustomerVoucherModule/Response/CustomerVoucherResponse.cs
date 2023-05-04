using Library.Models;
using System;

namespace BE_Homnayangi.Modules.CustomerVoucherModule.Response
{
    public class CustomerVoucherResponse
    {
        public Guid VoucherId { get; set; }
        public Guid CustomerId { get; set; }
        public string VoucherName { get; set; }
        public Guid CustomerVoucherId { get; set; }
        public string CustomerName { get; set; }
        public decimal Discount { get; set; }
        public decimal MinimumOrderPrice { get; set; }
        public decimal MaximumOrderPrice { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int? Status { get; set; }
        public DateTime CreatedDate { get; set; } // ngày trao
        // voucherId, customerVoucherId, discount, min, max, validFrom, validTo, Status(voucher)
    }
}
