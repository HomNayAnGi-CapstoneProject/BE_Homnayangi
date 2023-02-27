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
using BE_Homnayangi.Modules.RewardModule.Interface;
using Library.PagedList;
using BE_Homnayangi.Modules.Utils;
using BE_Homnayangi.Modules.RewardModule.DTO.Request;
using Microsoft.AspNetCore.Authorization;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RewardsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRewardService _rewardService;

        public RewardsController(IRewardService rewardService, IMapper mapper)
        {
            _mapper = mapper;
            _rewardService = rewardService;
        }

        // GET: api/Rewards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reward>>> GetRewards([FromQuery] RewardFilterRequest request)
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

        // GET: api/Rewards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reward>> GetReward([FromRoute] Guid id)
        {
            var reward = await _rewardService.GetRewardByID(id);

            if (reward == null)
            {
                return NotFound();
            }

            return reward;
        }

        // PUT: api/Rewards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PutReward([FromRoute] Guid id, [FromBody] Reward reward)
        {
            if (id != reward.RewardId)
            {
                return BadRequest();
            }

            try
            {
                await _rewardService.UpdateReward(rewardUpdate: reward);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RewardExists(id))
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

        // POST: api/Rewards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<Reward>> PostReward([FromBody] Reward reward)
        {
            try
            {
                await _rewardService.AddNewReward(reward);
            }
            catch (DbUpdateException)
            {
                if (RewardExists(reward.RewardId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReward", new { id = reward.RewardId }, reward);
        }

        // DELETE: api/Rewards/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteReward([FromRoute] Guid id)
        {
            var reward = await _rewardService.GetRewardByID(id);
            if (reward == null)
            {
                return NotFound();
            }
            reward.Status = false;
            await _rewardService.UpdateReward(reward);

            return NoContent();
        }

        private bool RewardExists(Guid id)
        {
            return _rewardService.GetRewardByID(id).Result != null;
        }
    }
}
