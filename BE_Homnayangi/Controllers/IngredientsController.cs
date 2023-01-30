using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.DataAccess;
using Library.Models;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.IngredientModule.Request;
using AutoMapper;
using BE_Homnayangi.Modules.IngredientModule.IngredientDTO;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IMapper mapper, IIngredientService ingredientService)
        {
            _mapper = mapper;
            _ingredientService = ingredientService;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {
            var result = await _ingredientService.GetAllIngredient();

            return new JsonResult(new
            {
                total_results = result.Count(),
                result = result,
            });
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public ActionResult<Ingredient> GetIngredient(Guid id)
        {
            var ingredient = _ingredientService.GetIngredientByID(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return new JsonResult(new
            {
                result = ingredient
            });
        }

        // GET: api/Ingredients/5
        [HttpGet("type/{typeId}")]
        public async Task<ActionResult<Ingredient>> GetIngredientsByTypeId(Guid typeId)
        {
            var ingredients = await _ingredientService.GetIngredientsByTypeId(typeId);

            return new JsonResult(new
            {
                total_results = ingredients.Count,
                result = ingredients,
            });
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut()]
        public async Task<IActionResult> PutIngredient([FromBody] UpdatedIngredientRequest ingredient)
        {
            var mapperIngredient = _mapper.Map<IngredientRequest>(ingredient);
            bool isUpdated = await _ingredientService.UpdateIngredient(mapperIngredient);
            if (isUpdated)
            {
                return new JsonResult(new
                {
                    message = "Updated successfully!"
                });
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            bool isDeleted = await _ingredientService.DeleteIngredient(id);
            if (isDeleted)
            {
                return new JsonResult(new
                {
                    message = "Deleted successfully!"
                });
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(CreatedIngredientRequest newIg)
        {
            var mapperIngredient = _mapper.Map<IngredientRequest>(newIg);
            Guid ingredientId = await _ingredientService.CreateIngredient(mapperIngredient);
            if (!ingredientId.ToString().Equals(""))
            {
                return new JsonResult(new
                {
                    status = "success",
                    ingredient_id = ingredientId
                });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
