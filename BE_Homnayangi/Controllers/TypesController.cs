using AutoMapper;
using BE_Homnayangi.Modules.TypeModule.DTO;
using BE_Homnayangi.Modules.TypeModule.DTO.Request;
using BE_Homnayangi.Modules.TypeModule.Interface;
using BE_Homnayangi.Modules.TypeModule.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = Library.Models.Type;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class TypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITypeService _typeService;

        public TypesController(ITypeService typeService, IMapper mapper)
        {
            _mapper = mapper;
            _typeService = typeService;
        }



        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type>>> GetBadges([FromQuery] TypeFilterRequest request)
        {
            try
            {
                var response = await _typeService.GetAllPaged(request);
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

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Type>> GetType([FromRoute] Guid id)
        {
            var type = await _typeService.GetTypeByID(id);

            if (type == null)
            {
                return NotFound();
            }

            return type;
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<Type>> PostType([FromBody] CreateTypeRequest typeRequest)
        {

            var type = _mapper.Map<Type>(typeRequest);
            try
            {
                await _typeService.AddNewType(type);
            }
            catch
            {
                if (TypeExists(type.TypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetType", new { id = type.TypeId }, type);
        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> PutType([FromRoute] Guid id, [FromBody] UpdateTypeRequest typeRequest)
        {

            var type = _mapper.Map<Type>(typeRequest);
            if (id != type.TypeId)
            {
                return new JsonResult(new
                {
                    msg = "Type is not valid"
                });
            }

            try
            {
                await _typeService.UpdateType(type);
            }
            catch (Exception ex)
            {
                if (!TypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return new JsonResult(new
                    {
                        status = "failed",
                        message = ex.Message
                    });
                }
            }

            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteType([FromRoute] Guid id)
        {
            var type = await _typeService.GetTypeByID(id);
            if (type == null)
            {
                return NotFound();
            }
            type.Status = false;
            await _typeService.UpdateType(type);

            return NoContent();
        }

        [HttpGet("drop-down")]
        //[Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<IEnumerable<TypeDropdownResponse>>> GetTypesDropdown()
        {
            try
            {
                var response = await _typeService.GetTypeDropdown();
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

        private bool TypeExists(Guid id)
        {
            return _typeService.GetTypeByID(id).Result != null;
        }
    }
}
