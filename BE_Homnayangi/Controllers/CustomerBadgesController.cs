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
                var curentUserAccomplishments = _userService.GetCustomerById(currentUserId).Result.Accomplishments.Count();
                var curentUserOrders = _userService.GetCustomerById(currentUserId).Result.Orders.Count();
                var badgeConditionList = _badgeConditionService.GetBadgeConditions().Result.ToList();

                for (int i = 0; i < badgeConditionList.Count - 1; i++)
                {
                    var currentBadgeConditionAccomplishments = badgeConditionList[i].Accomplishments == null ? 0 : badgeConditionList[i].Accomplishments;
                    var currentBadgeConditionOrders = badgeConditionList[i].Orders == null ? 0 : badgeConditionList[i].Orders;
                    var nextBadgeConditionAccomplishments = badgeConditionList[i + 1].Accomplishments == null ? 0 : badgeConditionList[i + 1].Accomplishments;
                    var nextBadgeConditionOrders = badgeConditionList[i + 1].Orders == null ? 0 : badgeConditionList[i + 1].Orders;
                    var diffCurrentBadgeConditionAccompishments = currentBadgeConditionAccomplishments - curentUserAccomplishments;
                    var diffNextBadgeConditonAccomplishments = nextBadgeConditionAccomplishments - curentUserAccomplishments;
                    var diffCurrentBadgeConditionOrders = currentBadgeConditionOrders - curentUserOrders;
                    var diffNextBadgeConditonOrders = nextBadgeConditionOrders - curentUserOrders;
                    if (diffCurrentBadgeConditionAccompishments > diffNextBadgeConditonAccomplishments && diffCurrentBadgeConditionOrders > diffNextBadgeConditonOrders)
                    {
                        var temp = badgeConditionList[i];
                        badgeConditionList[i] = badgeConditionList[i + 1];
                        badgeConditionList[i + 1] = temp;
                    }

                }

                return Ok(badgeConditionList);
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
