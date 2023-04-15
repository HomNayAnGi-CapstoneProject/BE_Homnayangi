using AutoMapper;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface;
using BE_Homnayangi.Modules.CustomerBadgeModule.DTO;
using BE_Homnayangi.Modules.CustomerBadgeModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
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
        private readonly IUserService _userService;
        private readonly IBadgeConditionService _badgeConditionService;
        public CustomerBadgesController(ICustomerBadgeService customerBadgeService, IMapper mapper, IUserService userService, IBadgeConditionService badgeConditionService)
        {
            _mapper = mapper;
            _customerBadgeService = customerBadgeService;
            _userService = userService;
            _badgeConditionService = badgeConditionService;
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
        [HttpGet("badgeConditions")]
        public async Task<ActionResult<BadgeCondition>> GetCustomerBadgeConditon()
        {
            try
            {
                var currentUserId = _userService.GetCurrentUser(Request.Headers["Authorization"]).Id;
                var currentUser = await _userService.GetCustomer(currentUserId);
                var currentUserBadgeIds = currentUser.CustomerBadges.Select(cb => cb.BadgeId).ToList();
                var badgeConditions = _badgeConditionService.GetBadgeConditions().Result.ToList();
                var badgeConditionList = badgeConditions
        .Where(bc => !currentUserBadgeIds.Contains(bc.BadgeId))
        .ToList();


                return Ok(badgeConditionList);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("customer")]
        public async Task<ActionResult<IEnumerable<CustomerBadge>>> GetCustomerBadgesByCurrentCustomer()
        {
            try
            {
                var currentUserId = _userService.GetCurrentUser(Request.Headers["Authorization"]).Id;
                var response = await _customerBadgeService.GetBadgeByCusID(currentUserId);
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
