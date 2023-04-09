using System;

namespace BE_Homnayangi.Modules.VoucherModule.Response
{
    public class ViewVoucherResponse
    {
        public Guid VoucherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public decimal? Discount { get; set; }
        public decimal? MinimumOrderPrice { get; set; }
        public decimal? MaximumOrderPrice { get; set; }
        public string AuthorName { get; set; }
    }
}
