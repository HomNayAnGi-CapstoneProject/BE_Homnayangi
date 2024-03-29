﻿using AutoMapper;
using BE_Homnayangi.Modules.BadgeModule;
using BE_Homnayangi.Modules.BadgeModule.DTO.Request;
using BE_Homnayangi.Modules.BadgeModule.Interface;
using BE_Homnayangi.Modules.BadgeModule.Response;
using BE_Homnayangi.Modules.RewardModule.DTO.Request;
using BE_Homnayangi.Modules.Utils;
using BE_Homnayangi.Ultils.Quartz;
using Hangfire;
using Library.Models;
using Library.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BadgesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBadgeService _badgeService;

        public BadgesController(IBadgeService badgeService, IMapper mapper)
        {
            _mapper = mapper;
            _badgeService = badgeService;
        }

        // GET: api/Badges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Badge>>> GetBadges([FromQuery] BadgeFilterRequest request)
        {
            try
            {/*
                if (request.fromDate.HasValue && request.toDate.HasValue
                    && !DateTimeUtils.CheckValidFromAndToDate(request.fromDate.Value, request.toDate.Value))
                {
                    return new JsonResult(new
                    {
                        status = "failed"
                    });
                }
*/
                var response = await _badgeService.GetAllPaged(request);
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
                var response = await _badgeService.CheckExistedName(name);
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
            var reward = await _badgeService.GetBadgeByID(id);

            if (reward == null)
            {
                return NotFound();
            }

            return reward;
        }

        // PUT: api/Badges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PutBadge([FromBody] UpdateBadgeRequest updateBadge)
        {
            var badge = _mapper.Map<Badge>(updateBadge);
            try
            {
                await _badgeService.UpdateBadge(badgeUpdate: badge);
                return Ok("Update success");
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
        // POST: api/Badges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<Badge>> PostBadge([FromBody] CreateNewBadgeRequest newBadge)
        {
            var badge = _mapper.Map<Badge>(newBadge);
            try
            {
                await _badgeService.AddNewBadge(badge);
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }

            return CreatedAtAction("GetBadge", new { id = badge.BadgeId }, badge);
        }

        // DELETE: api/Badges/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteBadge([FromRoute] Guid id)
        {
            try
            {
                await _badgeService.DeleteBadge(id);
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

        [HttpGet("dropdown")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BadgeDropdownResponse>>> GetBadgesDropdown()
        {
            try
            {
                var response = await _badgeService.GetBadgeDropdown();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    message = ex.Message
                });
            }
        }
        [HttpGet("begin-badge")]
        public IActionResult AwardBadgeToUser()
        {
            try
            {
                _badgeService.AwardBadge();
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
