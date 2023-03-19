using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Response;
using Library.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public HomeController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("subCategory/{subCateId}/blogs")]
        public async Task<ActionResult<IEnumerable<OverviewBlogResponse>>> GetBlogsBySubCate([FromRoute] Guid? subCateId)
        {
            if (subCateId == null)
            {
                return BadRequest();
            }

            var blogs = await _blogService.GetBlogsBySubCateForHomePage(subCateId, numberOfItems: (int)NumberItem.NumberItemShowEnum.EATING_STYLE);

            return new JsonResult(new
            {
                total_results = blogs.Count(),
                result = blogs,
            });
        }

        [HttpGet("blogs/cheap-price")]
        public async Task<ActionResult<IEnumerable<OverviewBlogResponse>>> GetBlogsByCheapPrice()
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
                title = Regex.Replace(title, @"\s+", " ");
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
    }
}

