using AutoMapper;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Request;
using BE_Homnayangi.Modules.UserModule.Interface;
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
        private readonly IUserService _userService;
        private readonly IVoucherService _voucherService;
        private readonly ICustomerVoucherService _customerVoucherService;

        public VouchersController(IMapper mapper, IVoucherService voucherService, ICustomerVoucherService customerVoucherService,
            IUserService userService)
        {
            _mapper = mapper;
            _voucherService = voucherService;
            _userService = userService;
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

        [Authorize(Roles = "Staff,Manager")]
        [HttpGet("active-vouchers")]
        public async Task<ActionResult> GetAllAvailableVouchers()
        {
            try
            {
                var result = await _voucherService.GetAllActiveVoucher();
                return new JsonResult(new
                {
                    status = "success",
                    result = result
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
            try
            {
                var currentAccount = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                bool isUpdated = await _voucherService.UpdateVoucher(currentAccount.Id, voucher);
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

        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PostVoucher([FromBody] CreateVoucherRequest voucher)
        {
            try
            {
                var currentAccount = _userService.GetCurrentUser(Request.Headers["Authorization"]);

                bool isUpdated = await _voucherService.CreateByUser(currentAccount.Id, voucher);
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
