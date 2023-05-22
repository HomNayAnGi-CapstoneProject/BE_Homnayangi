using AutoMapper;
using BE_Homnayangi.Modules.CategoryModule.Request;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BE_Homnayangi.Modules.RegionModule.Interface;
using BE_Homnayangi.Modules.RegionModule.Response;
using FluentValidation.Results;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IRegionService _regionService;

        public RegionController(IMapper mapper, IRegionService regionService)
        {
            _mapper = mapper;
            _regionService = regionService;
        }


        // GET: api/Regions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
        {
            return Ok(await _regionService.GetAll());
        }

        // GET: api/Regions/available
        [HttpGet("dropdown-region")]
        public async Task<ActionResult<IEnumerable<DropdownRegion>>> GetDropdownsAvailable()
        {
            var result = await _regionService.GetDropdownRegion();
            return new JsonResult(new
            {
                total = result.Count,
                result = result,
            });
        }

        // GET: api/Regions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> GetRegion(Guid id)
        {
            var region = _regionService.GetRegionByID(id);

            if (region == null)
            {
                return NotFound();
            }

            return region;
        }

        // PUT: api/Regions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PutRegion(UpdateRegionRequest region)
        {
            ValidationResult result = new UpdateRegionRequestValidator().Validate(region);
            if (!result.IsValid)
            {
                return new JsonResult(new
                {
                    status = "failed"
                });
            }

            if (await _regionService.UpdateRegion(region) == false) return NotFound();

            return Ok();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<Region>> PostRegion(CreateRegionRequest reqRegion)
        {
            ValidationResult result = new CreateRegionRequestValidator().Validate(reqRegion);
            if (!result.IsValid)
            {
                return new JsonResult(new
                {
                    status = "failed",
                });
            }

            return Ok(await _regionService.AddNewRegion(reqRegion));
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            await _regionService.DeleteRegion(id);
            return Ok();
        }
    }
}
