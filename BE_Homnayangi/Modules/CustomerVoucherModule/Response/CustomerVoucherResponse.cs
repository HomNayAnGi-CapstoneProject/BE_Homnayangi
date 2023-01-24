using System;

namespace BE_Homnayangi.Modules.CustomerVoucherModule.Response
{
    public class CustomerVoucherResponse
    {
        public string CustomerName { get; set; }
        public string VoucherName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Quantity { get; set; }
    }
}
