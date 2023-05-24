using AutoMapper;
using BE_Homnayangi.Modules.CategoryModule.Request;
using BE_Homnayangi.Modules.CookingMethodModule.Interface;
using BE_Homnayangi.Modules.CookingMethodModule.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using Library.Models;
using BE_Homnayangi.Modules.CookingMethodModule.Request;
using FluentValidation.Results;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CookingMethodController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICookingMethodService _cookingMethodService;

        public CookingMethodController(IMapper mapper, ICookingMethodService cookingMethodService)
        {
            _mapper = mapper;
            _cookingMethodService = cookingMethodService;
        }


        // GET: api/CookingMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CookingMethod>>> GetCookingMethods()
        {
            return Ok(await _cookingMethodService.GetAll());
        }

        // GET: api/CookingMethods/available
        [HttpGet("dropdown-cooking-method")]
        public async Task<ActionResult<IEnumerable<DropdownCookingMethod>>> GetDropdownsAvailable()
        {
            var result = await _cookingMethodService.GetDropdownCookingMethod();
            return new JsonResult(new
            {
                total = result.Count,
                result = result,
            });
        }

        // GET: api/CookingMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CookingMethod>> GetCookingMethod(Guid id)
        {
            var cookingMethod = _cookingMethodService.GetCookingMethodByID(id);

            if (cookingMethod == null)
            {
                return NotFound();
            }

            return cookingMethod;
        }

        // PUT: api/CookingMethods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PutCookingMethod(UpdateCookingMethodRequest cookingMethodRequest)
        {
            ValidationResult result = new UpdateCookingMethodRequestValidator().Validate(cookingMethodRequest);
            if (!result.IsValid)
            {
                return new JsonResult(new
                {
                    status = "failed"
                });
            }

            if (await _cookingMethodService.UpdateCookingMethod(cookingMethodRequest) == false) return NotFound();

            return Ok();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<CookingMethod>> PostCookingMethod(CreateCookingMethodRequest reqCookingMethod)
        {
            ValidationResult result = new CreateCookingMethodRequestValidator().Validate(reqCookingMethod);
            if (!result.IsValid)
            {
                return new JsonResult(new
                {
                    status = "failed",
                });
            }

            return Ok(await _cookingMethodService.AddNewCookingMethod(reqCookingMethod));
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteCookingMethod(Guid id)
        {
            await _cookingMethodService.DeleteCookingMethod(id);
            return Ok();
        }
    }
}
