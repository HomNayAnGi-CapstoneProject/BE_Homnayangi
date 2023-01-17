using AutoMapper;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Request;
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
        [HttpGet("customer/{cusId}/vouchers")]
        public async Task<ActionResult<IEnumerable<CustomerVoucher>>> GetAllCustomerVouchersByCusId([FromQuery]Guid cusId)
        {
            var result = await _customerVoucherService.GetAllCustomerVouchersByCusId(cusId);
            return new JsonResult(new
            {
                total_results = result.Count,
                result = result,
            });
        }

        [HttpPost]
        public async Task<IActionResult> PostVoucher([FromBody]CreatedCustomerVoucherRequest cv)
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
