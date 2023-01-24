using System;

namespace BE_Homnayangi.Modules.CustomerVoucherModule.Request
{
    public class CreatedCustomerVoucherRequest
    {
        public Guid VoucherId { get; set; }
        public Guid CustomerId { get; set; }
        public int? Quantity { get; set; }
    }
}
