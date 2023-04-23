using AutoMapper;
using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.IngredientModule.Request;
using BE_Homnayangi.Modules.IngredientModule.Response;
using Library.Models;
using Library.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        // GET: api/v1/Ingredients
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

        // GET: api/v1/Ingredients
        [HttpGet("customer")]
        public async Task<ActionResult<PagedResponse<PagedList<IngredientResponse>>>> GetIngredientsWithPagination([FromQuery] IngredientsByTypeRequest request)
        {
            var result = await _ingredientService.GetAllIngredientsWithPagination(request);

            return new JsonResult(new
            {
                result = result,
            });
        }

        // GET: api/Ingredients
        [HttpGet("managing")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredientsForStaff([FromQuery(Name = "searchString")] string? searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = Regex.Replace(searchString, @"\s+", " ").Trim();
                var result = await _ingredientService.GetAll(searchString);
                return new JsonResult(new
                {
                    total_results = result.Count(),
                    result = result,
                });
            }
            else
            {
                var result = await _ingredientService.GetAll(null);
                return new JsonResult(new
                {
                    total_results = result.Count(),
                    result = result,
                });
            }
        }

        // GET: api/v1/Ingredients
        [HttpGet("types/{typeId}/ingredients/{currentIngredientId}")]
        public async Task<ActionResult> GetIngredientsWithPagination([FromRoute] Guid typeId, [FromRoute] Guid currentIngredientId)
        {
            try
            {
                var result = await _ingredientService.GetIngredientsByTypeId(typeId, currentIngredientId);

                if (result.Count == 0)
                {
                    return new JsonResult(new
                    {
                        status = "failed",
                        msg = "Do not have any ingredient"
                    });
                }

                return new JsonResult(new
                {
                    status = "success",
                    result = result
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
        [HttpPut]
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
                return new JsonResult(new
                {
                    status = "failed"
                });
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
                return new JsonResult(new
                {
                    status = "failed"
                });
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
                return new JsonResult(new
                {
                    status = "failed"
                });
            }
        }
        [HttpGet("ingredient-searching")]
        public async Task<ActionResult<IEnumerable<SearchIngredientsResponse>>> GetBlogAndRecipeByName([FromQuery(Name = "name")] string name)
        {
            if (name is string && !String.IsNullOrEmpty(name))
            {
                name = Regex.Replace(name.Trim(), @"\s+", " ");
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
                return new JsonResult(new
                {
                    status = "failed"
                });
            }
        }
    }
}
