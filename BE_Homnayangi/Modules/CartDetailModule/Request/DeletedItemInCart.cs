using System;

namespace BE_Homnayangi.Modules.CartDetailModule.Request
{
    public class DeletedItemInCart
    {
        public Guid CartId { get; set; }
        public Guid ItemId { get; set; }
        public string TypeItem { get; set; }
    }
}
