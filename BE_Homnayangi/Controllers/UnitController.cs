using BE_Homnayangi.Modules.UnitModule.Interface;
using BE_Homnayangi.Modules.UnitModule.Request;
using BE_Homnayangi.Modules.UnitModule.Response;
using FluentValidation.Results;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }



        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            try
            {
                var response = await _unitService.GetAll();
                return Ok(response);
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

        [HttpGet("drop-down")]
        public async Task<ActionResult<IEnumerable<UnitDropdownResponse>>> GetUnitsDropdown()
        {
            try
            {
                var response = await _unitService.GetUnitDropdowns();
                return Ok(response);
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

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit([FromRoute] Guid id)
        {
            var unit = await _unitService.GetUnitByID(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<Unit>> CreateNewUnit([FromBody] CreateUnitRequest createUnitRequest)
        {
            ValidationResult result = new CreateUnitRequestValidator().Validate(createUnitRequest);
            if (!result.IsValid)
            {
                return new JsonResult(new
                {
                    msg = "Not valid data"
                });
            }

            await _unitService.AddNewUnit(createUnitRequest);

            return Ok();
        }


        // PUT api/<ValuesController>/5
        [HttpPut]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PutUnit([FromBody] UpdateUnitRequest unitRequest)
        {
            var unit = await _unitService.GetUnitByID(unitRequest.UnitId);
            if (unit == null)
            {
                return NotFound();
            }

            await _unitService.UpdateUnit(unitRequest);

            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteUnit([FromRoute] Guid id)
        {
            var unit = await _unitService.GetUnitByID(id);
            if (unit == null)
            {
                return NotFound();
            }
            await _unitService.DeleteUnit(unit);

            return Ok();
        }
    }
}
