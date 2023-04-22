using System;

namespace BE_Homnayangi.Modules.CustomerVoucherModule.Request
{
    public class GiveVoucherForCustomer
    {
        public Guid CustomerId { get; set; }
        public Guid VoucherId { get; set; }
    }
}
