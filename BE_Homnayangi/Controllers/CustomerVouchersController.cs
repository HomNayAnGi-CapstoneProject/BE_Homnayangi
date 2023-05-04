using AutoMapper;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Request;
using BE_Homnayangi.Modules.OrderModule.Interface;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class CustomerVouchersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly ICustomerVoucherService _customerVoucherService;

        public CustomerVouchersController(IMapper mapper, IOrderService orderService, ICustomerVoucherService customerVoucherService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _customerVoucherService = customerVoucherService;
        }

        // GET: api/v1/CustomerVouchers
        [HttpGet("customer/{cusId}/vouchers")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> GetAllCustomerVouchersByCusId([FromRoute] Guid cusId)
        {
            var result = await _customerVoucherService.GetAllCustomerVouchersByCusId(cusId);
            if (result != null)
            {
                return new JsonResult(new
                {
                    total_results = result.Count,
                    result = result,
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "failed",
                });
            }
        }

        //[HttpPost] // base api
        //[Authorize(Roles = "Staff,Manager")]
        //public async Task<IActionResult> PostVoucher([FromBody] CreatedCustomerVoucherRequest cv)
        //{
        //    var mappedObject = _mapper.Map<CustomerVoucher>(cv);
        //    var result = await _customerVoucherService.AddCustomerVoucher(mappedObject);
        //    if (result != null)
        //    {
        //        return new JsonResult(new
        //        {
        //            status = "success",
        //            result = result
        //        });
        //    }
        //    else
        //    {
        //        return new JsonResult(new
        //        {
        //            status = "failed",
        //            data = cv
        //        });
        //    }
        //}

        // POST: api/v1/vouchers/5
        [HttpPost("voucher-giving")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GiveVoucherForCustomer([FromBody] GiveVoucherForCustomer request)
        {
            try
            {
                await _customerVoucherService.GiveVoucherForCustomer(request);
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        [HttpGet("begin-voucher")]
        public IActionResult AwardBadgeToUser()
        {
            try
            {
                _customerVoucherService.AwardVoucher();
                return Ok();
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }
    }
}
