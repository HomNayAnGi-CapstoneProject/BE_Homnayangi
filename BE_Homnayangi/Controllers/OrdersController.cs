using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BE_Homnayangi.Modules.OrderModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Request;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.Utils;
using BE_Homnayangi.Modules.VoucherModule.Response;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrdersController(IMapper mapper, IOrderService orderService, IUserService userService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _userService = userService;
        }

        #region Get all orders for staff include deleted, without paging
        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            try
            {
                var res = await _orderService.GetAll();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get all orders by customer, without paging
        // GET: api/Orders/5
        [HttpGet("/Customer/{id}")]
        public async Task<ActionResult<Order>> GetByCustomer([FromRoute] Guid id)
        {
            try
            {
                var res = await _orderService.GetByCustomer(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateOrderRequest request)
        {
            try
            {
                Order order = _mapper.Map<Order>(request);
                //order.CustomerId = _userService.GetCurrentUser(Request.Headers["Authorization"]).Id;
                order.CustomerId = new Guid("31A1C0DF-178D-40AA-96F1-BC932E482D22"); // test

                await _orderService.AddNewOrder(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Orders/5
        //[HttpPut("payment/{id}")]
        //public async Task<ActionResult> Put([FromBody] PaymentRequest request)
        //{
        //    try
        //    {
        //        _orderService.UpdateOrderPaymentStatus(request);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("cart")]
        public async Task<ActionResult<Order>> GetCart()
        {
            try
            {
                //var customerID = _userService.GetCurrentUser(Request.Headers["Authorization"]).Id;
                var customerID = new Guid("31A1C0DF-178D-40AA-96F1-BC932E482D22"); //test
                var cart = await _orderService.GetCart(customerID);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("cart")]
        public async Task<ActionResult> UpdateCart([FromBody] Order order)
        {
            try
            {
                var customerId = order.CustomerId;
                if (!customerId.HasValue)
                    throw new Exception("Order not binded to any customer");

                await _orderService.UpdateCart(order);
                return Ok("Update cart successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("checkout/{id}")]
        public async Task<ActionResult> Checkout([FromRoute] Guid id)
        {
            try
            {
                var url = await _orderService.Checkout(id);
                return Ok(url);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("accept/{id}")]
        public async Task<ActionResult> AcceptOrder([FromRoute] Guid id)
        {
            try
            {
                await _orderService.AcceptOrder(id);
                return Ok("Order accepted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
