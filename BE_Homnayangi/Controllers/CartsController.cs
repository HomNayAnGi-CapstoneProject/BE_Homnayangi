using BE_Homnayangi.Modules.CartDetailModule.Interface;
using BE_Homnayangi.Modules.CartDetailModule.Request;
using BE_Homnayangi.Modules.CartModule.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICartDetailService _cartDetailService;

        public CartsController(ICartService cartService, ICartDetailService cartDetailService)
        {
            _cartService = cartService;
            _cartDetailService = cartDetailService;
        }

        // GET: api/Carts/E7E1BD28-8979-4B6E-ADBC-458408E6BA41
        [HttpGet("{customerId}")]
        public async Task<ActionResult> GetCartDetailsByCustomerId([FromRoute] Guid customerId)
        {
            var cart = _cartService.GetCartByCustomerId(customerId);
            if (cart == null)
            {
                return new JsonResult(new
                {
                    total_items_in_cart = 0
                });
            }
            else
            {
                var cart_details = await _cartDetailService.GetCartDetailsByCartId(cart.CartId);
                return new JsonResult(new
                {
                    total_items_in_cart = cart_details.Count,
                    cart_details = cart_details
                });
            }
        }

        // PUT: api/v1/carts
        [HttpPut("update-quantity-item")]
        public async Task<ActionResult> UpdateQuantityItemInCart([FromBody] UpdatedItemInCart updatedItemInCart)
        {
            var result = await _cartDetailService.UpdateQuantityItemInCart(updatedItemInCart);
            if (result == null)
            {
                return new JsonResult(new
                {
                    status = "failed"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "success",
                    cart_id = updatedItemInCart.CartId,
                    item_id = updatedItemInCart.ItemId,
                    quantity = updatedItemInCart.Quantity,
                });
            }
        }

        // DELETE: api/v1/carts
        [HttpDelete]
        public async Task<ActionResult> UpdateQuantityItemInCart([FromBody]DeletedItemInCart item)
        {
            var isDeleted = await _cartDetailService.DeleteItemInCart(item.CartId, item.ItemId, item.TypeItem);
            if (!isDeleted)
            {
                return new JsonResult(new
                {
                    status = "failed"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "success"
                });
            }
        }

        // POST: api/v1/carts
        [HttpPost]
        public async Task<ActionResult> AddNewItemIntoCart([FromBody] InsertedItemIntoCart newItem)
        {
            var result = await _cartService.InsertNewItemIntoCart(newItem);

            if (result == null)
            {
                return new JsonResult(new
                {
                    status = "failed"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "success"
                });
            }
        }
    }
}
