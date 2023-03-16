using BE_Homnayangi.Modules.BlogReactionModule.Interface;
using BE_Homnayangi.Modules.BlogReactionModule.Response;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
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
        private readonly IUserService _userService;

        public BlogReactionsController(IBlogReactionService blogReactionService, IUserService userService)
        {
            _blogReactionService = blogReactionService;
            _userService = userService;
        }

        [HttpGet("blogs/{blogId}")]
        public async Task<ActionResult<BlogReaction>> GetBlogReactionByBlogAndCustomerId([FromRoute] Guid blogId)
        {
            try
            {
                var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                if (currentUser == null) throw new Exception(ErrorMessage.CommonError.NOT_LOGIN); 
                var result = await _blogReactionService.GetBlogReactionByBlogAndCustomerId(blogId, currentUser.Id);
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
                //return BadRequest(ex.Message);
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        [HttpPut("blogs/{blogId}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<BlogReactionResponse>> InteractWithBlog([FromRoute] Guid blogId)
        {
            try
            {
                var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                var result = await _blogReactionService.InteractWithBlog(blogId, currentUser.Id);
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
