using AutoMapper;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.DTO.RecipeDTO;
using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
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
    public class RecipesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecipeService _recipeServices;
        private readonly IRecipeDetailService _recipeDetailService;
        private readonly IUserService _userService;

        public RecipesController(IRecipeService recipeServices, IRecipeDetailService recipeDetailService,
            IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _recipeServices = recipeServices;
            _recipeDetailService = recipeDetailService;
            _userService = userService;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            var recipes = await _recipeServices.GetRecipesBy(includeProperties: "RecipeDetails");
            recipes.ToList();

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

        #region DELETE - RESTORE RECIPE
        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteRecipe([FromRoute] Guid id)
        {
            try
            {
                #region Authorization
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (_userService.GetCurrentUser(Request.Headers["Authorization"]).Role.Equals("Customer"))
                {
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_ALLOWED_TO_DELETE_RECIPE);
                }
                else if (_userService.GetCurrentUser(Request.Headers["Authorization"]).Role.Equals("Admin"))
                {
                    throw new Exception(ErrorMessage.AdminError.ADMIN_NOT_ALLOWED_TO_DELETE_RECIPE);
                }
                #endregion

                await _recipeServices.DeleteRecipe(id);
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("restore-recipe/{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> RestoreRecipe([FromRoute] Guid id)
        {
            try
            {
                #region Authorization
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (_userService.GetCurrentUser(Request.Headers["Authorization"]).Role.Equals("Customer"))
                {
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_ALLOWED_TO_RESTORE_RECIPE);
                }
                else if (_userService.GetCurrentUser(Request.Headers["Authorization"]).Role.Equals("Admin"))
                {
                    throw new Exception(ErrorMessage.AdminError.ADMIN_NOT_ALLOWED_TO_RESTORE_RECIPE);
                }
                #endregion

                await _recipeServices.RestoreRecipe(id);
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
