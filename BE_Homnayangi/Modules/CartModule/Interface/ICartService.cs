using BE_Homnayangi.Modules.CartDetailModule.Request;
using Library.Models;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CartModule.Interface
{
    public interface ICartService
    {
        public Cart GetCartByCustomerId(Guid customerId);
        public Task<Cart> InsertNewItemIntoCart(InsertedItemIntoCart newItem);
    }
}
