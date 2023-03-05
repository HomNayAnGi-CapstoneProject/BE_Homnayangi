using BE_Homnayangi.Modules.BlogReactionModule.Interface;
using BE_Homnayangi.Modules.BlogReactionModule.Response;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPut("blogs/{blogId}/customers/{customerId}")]
        [Authorize(Roles = "Staff,Manager,Customer")]
        public async Task<ActionResult<BlogReactionResponse>> InteractWithBlog([FromRoute] Guid blogId, [FromRoute] Guid customerId)
        {
            try
            {
                var result = await _blogReactionService.InteractWithBlog(blogId, customerId);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
