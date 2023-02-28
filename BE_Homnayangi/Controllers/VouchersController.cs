using AutoMapper;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.VoucherModule.Interface;
using BE_Homnayangi.Modules.VoucherModule.Request;
using BE_Homnayangi.Modules.VoucherModule.Response;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVoucherService _voucherService;
        private readonly ICustomerVoucherService _customerVoucherService;

        public VouchersController(IMapper mapper, IVoucherService voucherService, ICustomerVoucherService customerVoucherService)
        {
            _mapper = mapper;
            _voucherService = voucherService;
            _customerVoucherService = customerVoucherService;
        }

        // GET: api/v1/vouchers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewVoucherResponse>>> GetAllVouchers()
        {
            var result = await _voucherService.GetAllVoucher();
            return new JsonResult(new
            {
                total_results = result.Count(),
                result = result,
            });
        }

        // GET: api/v1/vouchers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voucher>> GetVoucherById([FromRoute] Guid id)
        {
            var result = await _voucherService.GetVoucherByID(id);
            return new JsonResult(new
            {
                result = result,
            });
        }

        [HttpPut]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PutVoucher([FromBody] UpdateVoucherRequest voucher)
        {
            var mappedVoucher = _mapper.Map<Voucher>(voucher);
            bool isUpdated = await _voucherService.UpdateVoucher(mappedVoucher);
            return new JsonResult(new
            {
                status = "success"
            });
        }

        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PostVoucher([FromBody] CreateVoucherRequest voucher)
        {
            var mappedVoucher = _mapper.Map<Voucher>(voucher);
            bool isUpdated = await _voucherService.CreateByUser(mappedVoucher);
            return new JsonResult(new
            {
                status = "success"
            });
        }

        // DELETE: api/v1/vouchers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteVoucher(Guid id)
        {
            bool isVoucherDeleted = await _voucherService.DeleteVoucherById(id);
            await _customerVoucherService.DeleteCustomerVouchersByVoucherId(id);
            if (isVoucherDeleted)
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
    }
}
