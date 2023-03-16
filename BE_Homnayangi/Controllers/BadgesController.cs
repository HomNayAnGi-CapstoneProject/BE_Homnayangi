using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.DataAccess;
using Library.Models;
using AutoMapper;
using BE_Homnayangi.Modules.BadgeModule.Interface;
using Library.PagedList;
using BE_Homnayangi.Modules.Utils;
using BE_Homnayangi.Modules.BadgeModule.DTO.Request;
using Microsoft.AspNetCore.Authorization;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BadgesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBadgeService _rewardService;

        public BadgesController(IBadgeService rewardService, IMapper mapper)
        {
            _mapper = mapper;
            _rewardService = rewardService;
        }

        // GET: api/Badges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Badge>>> GetBadges([FromQuery] BadgeFilterRequest request)
        {
            try
            {
                if (request.fromDate.HasValue && request.toDate.HasValue
                    && !DateTimeUtils.CheckValidFromAndToDate(request.fromDate.Value, request.toDate.Value))
                {
                    return BadRequest();
                }

                var response = await _rewardService.GetAllPaged(request);
                return Ok(response);
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet("existed-name")]
        public async Task<ActionResult<bool>> CheckExistedName([FromQuery] string name)
        {
            try
            {
                var response = await _rewardService.CheckExistedName(name);
                return Ok(response);
            }
            catch
            {
                return NoContent();
            }
        }

        // GET: api/Badges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Badge>> GetBadge([FromRoute] Guid id)
        {
            var reward = await _rewardService.GetBadgeByID(id);

            if (reward == null)
            {
                return NotFound();
            }

            return reward;
        }

        // PUT: api/Badges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PutBadge([FromRoute] Guid id, [FromBody] Badge reward)
        {
            if (id != reward.BadgeId)
            {
                return BadRequest();
            }

            try
            {
                await _rewardService.UpdateBadge(rewardUpdate: reward);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BadgeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Badges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<Badge>> PostBadge([FromBody] Badge reward)
        {
            try
            {
                await _rewardService.AddNewBadge(reward);
            }
            catch (DbUpdateException)
            {
                if (BadgeExists(reward.BadgeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBadge", new { id = reward.BadgeId }, reward);
        }

        // DELETE: api/Badges/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteBadge([FromRoute] Guid id)
        {
            var reward = await _rewardService.GetBadgeByID(id);
            if (reward == null)
            {
                return NotFound();
            }
            reward.Status = false;
            await _rewardService.UpdateBadge(reward);

            return NoContent();
        }

        private bool BadgeExists(Guid id)
        {
            return _rewardService.GetBadgeByID(id).Result != null;
        }
    }
}
