using BE_Homnayangi.Modules.CartDetailModule.Request;
using BE_Homnayangi.Modules.CartDetailModule.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CartDetailModule.Interface
{
    public interface ICartDetailService
    {
        public Task<ICollection<ItemInCart>> GetCartDetailsByCartId(Guid cartId);
        public Task<UpdatedItemInCart> UpdateQuantityItemInCart(UpdatedItemInCart updatedItemInCart);
        public Task<bool> DeleteItemInCart(Guid cartId, Guid itemId, string type);
    }
}
