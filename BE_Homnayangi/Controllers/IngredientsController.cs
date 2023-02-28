using AutoMapper;
using BE_Homnayangi.Modules.IngredientModule.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.IngredientModule.Request;
using BE_Homnayangi.Modules.IngredientModule.Response;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var result = await _ingredientService.GetAllIngredients();

            return new JsonResult(new
            {
                total_results = result.Count(),
                result = result,
            });
        }

        // GET: api/Ingredients
        [HttpGet("managing")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredientsForStaff()
        {
            var result = await _ingredientService.GetAll();

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
        [Authorize(Roles = "Staff,Manager")]
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
        [Authorize(Roles = "Staff,Manager")]
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
        [Authorize(Roles = "Staff,Manager")]
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
        [HttpGet("ingredient-searching")]
        public async Task<ActionResult<IEnumerable<SearchIngredientsResponse>>> GetBlogAndRecipeByName([FromQuery(Name = "name")] string name)
        {
            if (name is string && !String.IsNullOrEmpty(name))
            {
                name = name.TrimStart(' ');
                var result = await _ingredientService.GetIngredientByName(name);
                if (result.Any())
                {
                    return new JsonResult(new
                    {
                        result = result,
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        result = "",
                    });
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
