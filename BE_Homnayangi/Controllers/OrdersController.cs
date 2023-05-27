using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BE_Homnayangi.Modules.NotificationModule.Interface;
using BE_Homnayangi.Modules.NotificationModule.Request;
using BE_Homnayangi.Modules.OrderModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Request;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.Utils;
using BE_Homnayangi.Modules.VoucherModule.Response;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using static Google.Apis.Requests.BatchRequest;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using BE_Homnayangi.Modules.OrderModule.Response;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IHubContext<SignalRServer> _hubContext;
        private readonly INotificationService _notificationService;

        public OrdersController(IMapper mapper, IOrderService orderService, IUserService userService, IHubContext<SignalRServer> hubContext, INotificationService notificationService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _userService = userService;
            _hubContext = hubContext;
            _notificationService = notificationService;
        }

        #region Get all orders for staff include deleted, without paging
        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult> GetAll()
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
                order.CustomerId = _userService.GetCurrentUser(Request.Headers["Authorization"]).Id;

                var redirectUrl = await _orderService.AddNewOrder(order);
                return Ok(redirectUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("paid/{id}")]
        public async Task<ActionResult> PaidOrder([FromRoute] Guid id)
        {
            try
            {
                await _orderService.PaidOrder(id);
                return Ok("Order had paid");
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

        [HttpPut("deny/{id}")]
        public async Task<ActionResult> DenyAsync([FromRoute] Guid id)
        {
            try
            {
                await _orderService.DenyOrder(id);
                return Ok("Order denied");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("cancel/{id}")]
        public async Task<ActionResult> Cancel([FromRoute] Guid id)
        {
            try
            {
                await _orderService.CancelOrder(id);
                return Ok("Order cancelled");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("refund/{id}")]
        public async Task<ActionResult> Refund([FromRoute] Guid id)
        {
            try
            {
                await _orderService.RefundOrder(id);
                return Ok("Order refunded");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("shipping/{id}")]
        public async Task<ActionResult> Shipping([FromRoute] Guid id)
        {
            try
            {
                await _orderService.Shipping(id);
                return Ok("Shipping");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delivered/{id}")]
        public async Task<ActionResult> Delivered([FromRoute] Guid id, [FromQuery] bool fail)
        {
            try
            {
                if (fail)
                    await _orderService.DeliveredFail(id);
                else
                    await _orderService.Delivered(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("local")]
        public ActionResult GetLocalDistrict()
        {
            try
            {
                var res = _orderService.GetLocalDistrict();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("local/{district}")]
        public ActionResult GetLocalWard([FromRoute] string district)
        {
            try
            {
                var res = _orderService.GetLocalWard(district);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("status")]
        public async Task<ActionResult> GetOrderResponse([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] Guid? customerid, [FromQuery] int status = -1)
        {
            try
            {
                var res = customerid == null
                    ? await _orderService.GetOrderResponse(fromDate, toDate, status)
                    : await _orderService.GetOrderByCustomer(fromDate, toDate, customerid.Value, status);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("status/customer")]
        public async Task<ActionResult> GetOrderResponseByCustomer([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] int status = -1)
        {
            try
            {
                var customerId = _userService.GetCurrentUser(Request.Headers["Authorization"]).Id;
                if (customerId.Equals(Guid.Empty))
                    throw new Exception("Require login");
                var res = await _orderService.GetOrderByCustomer(fromDate, toDate, customerId, status);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("report/month")]
        public async Task<ActionResult> GetMonthReport([FromQuery] int month, [FromQuery] int year)
        {
            try
            {
                var res = await _orderService.CreateMonthlyFinancialReport(month, year);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("report")]
        public async Task<ActionResult> GetReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var res = await _orderService.CreateFinancialReport(startDate, endDate);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("report/export")]
        public async Task<ActionResult> ExportReport([FromQuery] int month, [FromQuery] int year)
        {
            var res = await _orderService.CreateMonthlyFinancialReport(month, year);
            var csv = res.TrendingPackages;

            //var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            //    {
            //    NewLine = Environment.NewLine,
            //    Encoding = Encoding.UTF8
            //    };

                using (var mem = new MemoryStream())
                using (var writer = new StreamWriter(mem))
                using (var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csvWriter.WriteHeader<TrendingPackage>();
                    csvWriter.WriteRecords(csv);

                    writer.Flush();
                    var result = Encoding.UTF8.GetString(mem.ToArray());
                    Console.WriteLine(result);
                    //var fileBytes = Encoding.UTF8.GetBytes(mem.ToArray());
                    return File(mem.ToArray(), "text/csv", "Report.csv");
                }

            
        }
    }
}
