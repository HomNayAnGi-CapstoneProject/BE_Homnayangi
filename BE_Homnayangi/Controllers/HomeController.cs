using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.DataAccess;
using System.Linq;
using BE_Homnayangi.Modules.BlogModule.Response;
using Library.Models.Enum;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly HomnayangiContext _context;
        private readonly IBlogService _blogService;
        private readonly IRecipeService _recipeService;

        public HomeController(HomnayangiContext context, IBlogService blogService, IRecipeService recipeService)
        {
            _context = context;
            _blogService = blogService;
            _recipeService = recipeService;
        }

        [HttpGet("tag/{tagId}/blogs")]
        public async Task<ActionResult<IEnumerable<GetBlogsForHomePageResponse>>> GetBlogsByTag(string tagId)
        {
            if (!Guid.TryParse(tagId, out var _tagId))
            {
                return BadRequest();
            }

            var blogs = await _blogService.GetBlogsByTagForHomePage(_tagId, numberOfItems: (int)NumberItem.NumberItemShowEnum.EATING_STYLE);

            return new JsonResult(new
            {
                total_results = blogs.Count(),
                result = blogs,
            });
        }

        [HttpGet("blogs/cheap-price")]
        public async Task<ActionResult<IEnumerable<GetBlogsForHomePageResponse>>> GetBlogsByCheapPrice()
        {
            var result = await _blogService.GetBlogsSortByPackagePriceAsc();
            return new JsonResult(new
            {
                total_results = result.Count(),
                result = result,
            });
        }

        [HttpGet("blogs/live-searching")]
        public async Task<ActionResult<IEnumerable<SearchBlogsResponse>>> GetBlogAndRecipeByName([FromQuery(Name = "title")] string title)
        {
            if (title != "" && title != null && title is string)
            {
                title = title.TrimStart(' ');
                var result = await _blogService.GetBlogAndRecipeByName(title);
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

        [HttpGet("category/{categoryId}/blog-menu")]
        public async Task<ActionResult<IEnumerable<GetBlogsForHomePageResponse>>> GetSoupAndNormalBlogs(string categoryId)
        {
            if (!Guid.TryParse(categoryId, out var _categoryId))
            {
                return BadRequest();
            }

            var result = await _blogService.GetSoupAndNormalBlogs(_categoryId);
            if (result == null)
            {
                return new JsonResult(new
                {
                    title = "Bad Request",
                    status = 400,
                    message = "Sorry, our service can not provided at this time!"
                });
            }
            return new JsonResult(new
            {
                total_results = result.Count(),
                result = result,
            });
        }
    }
}

