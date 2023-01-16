using AutoMapper;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Request;
using BE_Homnayangi.Modules.VoucherModule.Request;
using Library.Models;
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
        private readonly ICustomerVoucherService _customerVoucherService;

        public CustomerVouchersController(IMapper mapper, ICustomerVoucherService customerVoucherService)
        {
            _mapper = mapper;
            _customerVoucherService = customerVoucherService;
        }

        // GET: api/v1/CustomerVouchers
        [HttpGet("customer/{cusId}")]
        public async Task<ActionResult<IEnumerable<CustomerVoucher>>> GetAllCustomerVouchersByCusId(Guid cusId)
        {
            var result = await _customerVoucherService.GetAllCustomerVouchersByCusId(cusId);
            return new JsonResult(new
            {
                total_results = result.Count,
                result = result,
            });
        }

        // DELETE: api/v1/CustomerVouchers/5
        [HttpDelete("voucher/{id}")]
        public async Task<IActionResult> DeleteVoucher(Guid id)
        {
            bool isDeleted = await _customerVoucherService.DeleteCustomerVouchersByVoucherId(id);
            if (isDeleted)
            {
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "failed"
                });
            }
        }

        // DELETE: api/v1/CustomerVouchers/5
        [HttpDelete("voucher/{voucherId}/{customerId}")]
        public async Task<IActionResult> DeleteVoucher(Guid voucherId, Guid customerId)
        {
            bool isDeleted = await _customerVoucherService.DeleteCustomerVouchersByCusAndVoucherId(voucherId, customerId);
            if (isDeleted)
            {
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "failed"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostVoucher(CreatedCustomerVoucherRequest cv)
        {
            var mappedObject = _mapper.Map<CustomerVoucher>(cv);
            var result = await _customerVoucherService.AddCustomerVoucher(mappedObject);
            if (result != null)
            {
                return new JsonResult(new
                {
                    status = "success",
                    result = result
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "failed",
                    data = cv
                });
            }
        }
    }
}
