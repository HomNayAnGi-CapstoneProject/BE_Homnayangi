using BE_Homnayangi.Modules.TypeModule.DTO;
using BE_Homnayangi.Modules.TypeModule.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BE_Homnayangi.Modules.UnitModule.Interface;
using Library.Models;
using BE_Homnayangi.Modules.UnitModule.Request;
using FluentValidation.Results;

namespace BE_Homnayangi.Controllers
{
    [Route("api/[controller]")]
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
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("drop-down")]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnitsDropdown()
        {
            try
            {
                var response = await _unitService.GetUnitDropdowns();
                return Ok(response);
            }
            catch
            {
                return BadRequest();
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
        public async Task<ActionResult<Unit>> CreateNewUnit([FromBody] CreateUnitRequest createUnitRequest)
        {
            ValidationResult result = new CreateUnitRequestValidator().Validate(createUnitRequest);
            if (!result.IsValid)
            {
                return BadRequest();
            }

            await _unitService.AddNewUnit(createUnitRequest);

            return Ok();
        }


        // PUT api/<ValuesController>/5
        [HttpPut]
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
