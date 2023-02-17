using System;

namespace BE_Homnayangi.Modules.CartDetailModule.Response
{
    public class ItemInCart
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
