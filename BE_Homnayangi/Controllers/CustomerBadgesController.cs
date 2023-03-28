using AutoMapper;
using BE_Homnayangi.Modules.CustomerBadgeModule.DTO;
using BE_Homnayangi.Modules.CustomerBadgeModule.Interface;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerBadgesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerBadgeService _customerBadgeService;

        public CustomerBadgesController(ICustomerBadgeService customerBadgeService, IMapper mapper)
        {
            _mapper = mapper;
            _customerBadgeService = customerBadgeService;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerBadge>>> GetCustomerBadges([FromQuery] CustomerBadgeFilterRequest request)
        {
            try
            {

                var response = await _customerBadgeService.GetAllPaged(request);
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{customerId}/{badgeId}")]
        public async Task<ActionResult<CustomerBadge>> GetCustomerBadge([FromRoute] Guid customerId, [FromRoute] Guid badgeId)
        {
            try
            {

                var customerBadge = await _customerBadgeService.GetCustomerBadgeByCombineID(customerId, badgeId);

                if (customerBadge == null)
                {
                    return NotFound();
                }

                return customerBadge;
            }
            catch
            {
                return BadRequest();
            }
        }
        // DELETE api/<ValuesController>/5
        [HttpDelete("{customerId}/{badgeId}")]
        public async Task<ActionResult> Delete(Guid customerId, Guid badgeId)
        {
            try
            {
                var isDeleted = await _customerBadgeService.DeleteCustomerBadgeByCombineId(customerId, badgeId);
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
            catch
            {
                return BadRequest();
            }
        }
    }
}
