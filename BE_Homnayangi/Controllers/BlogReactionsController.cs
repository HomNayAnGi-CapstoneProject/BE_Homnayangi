using BE_Homnayangi.Modules.BlogReactionModule.Interface;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogReactionsController : ControllerBase
    {
        private readonly IBlogReactionService _blogReactionService;

        public BlogReactionsController(IBlogReactionService blogReactionService)
        {
            _blogReactionService = blogReactionService;
        }

        [HttpGet("blogs/{blogId}/customers/{customerId}")]
        public async Task<ActionResult<BlogReaction>> GetBlogReactionByBlogAndCustomerId([FromRoute] Guid blogId, [FromRoute] Guid customerId)
        {
            var result = await _blogReactionService.GetBlogReactionByBlogAndCustomerId(blogId, customerId);
            if (result != null)
            {
                return new JsonResult(new
                {
                    status = "success",
                    result = result,
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "failed",
                    result = result,
                });
            }

        }

        [HttpPut("blogs/{blogId}/customers/{customerId}")] // thả tym
        public async Task<IActionResult> GiveReactionForBlog([FromRoute] Guid blogId, [FromRoute] Guid customerId)
        {
            var result = await _blogReactionService.GiveReactionForBlog(blogId, customerId);
            if (result != null)
            {
                return new JsonResult(new
                {
                    status = "success",
                    result = result,
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "failed",
                    result = result,
                });
            }
        }


        [HttpDelete("blogs/{blogId}/customers/{customerId}")] // xoá tym
        public async Task<IActionResult> RemoveReactionForBlog([FromRoute] Guid blogId, [FromRoute] Guid customerId)
        {
            var result = await _blogReactionService.RemoveReactionForBlog(blogId, customerId);
            if (result != null)
            {
                return new JsonResult(new
                {
                    status = "success",
                    result = result,
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "failed",
                    result = result,
                });
            }
        }
    }
}
