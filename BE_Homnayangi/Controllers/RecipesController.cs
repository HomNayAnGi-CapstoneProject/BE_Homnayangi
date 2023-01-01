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
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.Models.DTO.RecipeDTO;
using Library.Models.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using Library.Models.DTO.IngredientDTO;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecipeServices _recipeServices;
        private readonly IRecipeDetailService _recipeDetailService;
        private readonly IIngredientService _ingredientService;

        public RecipesController(IRecipeServices recipeServices, IRecipeDetailService recipeDetailService, IIngredientService ingredientService, IMapper mapper)
        {
            _mapper = mapper;
            _recipeServices = recipeServices;
            _recipeDetailService = recipeDetailService;
            _ingredientService = ingredientService;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            var recipeDetails = _recipeDetailService
                .GetRecipeDetailsBy(includeProperties: "Ingredient")
                .Result
                .Select(_mapper.Map<RecipeDetail, RecipeDetailsResponse>)
                .ToList();

            var recipes = await _recipeServices.GetRecipesBy(includeProperties: "RecipeNavigation, RecipeDetails");
            recipes.ToList().ForEach(r => r.RecipeNavigation.Recipe = null);

            var recipesResponse = recipes.Select(_mapper.Map<Recipe, RecipeResponse>);

            return Ok(recipesResponse);
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeResponse>> GetRecipe(Guid id)
        {
            var recipeDetails = _recipeDetailService
                .GetRecipeDetailsBy(rd => rd.RecipeId.Equals(id), includeProperties: "Ingredient")
                .Result
                .Select(_mapper.Map<RecipeDetail, RecipeDetailsResponse>)
                .ToList();

            var recipe = await _recipeServices.GetRecipesBy(r => r.RecipeId.Equals(id));

            var recipeResponse = _mapper.Map<Recipe, RecipeResponse>(recipe.FirstOrDefault());
            recipeResponse.RecipeDetails = recipeDetails;

            if (recipe == null)
            {
                return NotFound();
            }

            return recipeResponse;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(Guid id, Recipe recipe)
        {
            //if (id != recipe.RecipeId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(recipe).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!RecipeExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            //_context.Recipes.Add(recipe);
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (RecipeExists(recipe.RecipeId))
            //    {
                    return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            //var recipe = await _context.Recipes.FindAsync(id);
            //if (recipe == null)
            //{
            //    return NotFound();
            //}

            //_context.Recipes.Remove(recipe);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(Guid id)
        {
            //return _context.Recipes.Any(e => e.RecipeId == id);
            return false;
        }
    }
}
