using System;

namespace BE_Homnayangi.Modules.VoucherModule.Request
{
    public class UpdateVoucherRequest  : CreateVoucherRequest
    {
        public Guid VoucherId { get; set; }
        public int Status { get; set; }
    }
}
