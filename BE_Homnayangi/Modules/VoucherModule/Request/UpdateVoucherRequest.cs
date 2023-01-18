using System;

namespace BE_Homnayangi.Modules.VoucherModule.Request
{
    public class UpdateVoucherRequest
    {
        public Guid VoucherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal Discount { get; set; }
        public decimal MinimumOrder { get; set; }
        public decimal MaximumOrder { get; set; }
    }
}
