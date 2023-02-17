using System;

namespace BE_Homnayangi.Modules.CartDetailModule.Request
{
    public class InsertedItemIntoCart
    {
        public Guid CustomerId { get; set; }
        public Guid ItemId { get; set; }
        public bool TypeItem { get; set; }
    }
}
