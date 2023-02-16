using System;

namespace BE_Homnayangi.Modules.CartDetailModule.Request
{
    public class UpdatedItemInCart: DeletedItemInCart
    {
        public int Quantity { get; set; }
    }
}
