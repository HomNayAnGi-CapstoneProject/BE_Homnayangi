using BE_Homnayangi.Modules.CommentModule.Interface;
using BE_Homnayangi.Modules.CommentModule.Request;
using BE_Homnayangi.Modules.CommentModule.Response;
using BE_Homnayangi.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentsController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        // GET: api/v1/comments/57448A79-8855-42AD-BD2E-0295D1436037
        [HttpGet("{blogId}")]
        public async Task<ActionResult<List<Tuple<ParentComment, List<ChildComment>>>>> GetCommentsByBlogId([FromRoute] Guid blogId)
        {
            var result = await _commentService.GetCommentsByBlogId(blogId);
            if (result != null)
            {
                return new JsonResult(new
                {
                    total_comments = CountTotalComments(result),
                    result = result
                });
            }
            else
            {
                return new JsonResult(new
                {
                    total_comments = 0,
                    result = new List<ParentComment>()
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Customer,Staff,Manager")]
        public async Task<ActionResult<bool>> CreateANewComment([FromBody] CreatedCommentRequest newComment)
        {
            var user = _userService.GetCurrentUser(Request.Headers["Authorization"]);
            var result = await _commentService.CreateANewComment(newComment, user);
            if (result != null)
            {
                return new JsonResult(new
                {
                    status = "success",
                    newComment = result
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "fail",
                    newComment = result
                });
            }
        }

        [HttpDelete("{commentId}")]
        [Authorize(Roles = "Customer,Staff,Manager")]
        public async Task<ActionResult<ChildComment>> DeleteAComment([FromRoute] Guid commentId)
        {
            try
            {
                var user = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                var result = await _commentService.DeleteAComment(commentId, user.Id);
                if (result)
                {
                    return new JsonResult(new
                    {
                        status = "success"
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        status = "fail"
                    });
                }
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

        [HttpPut]
        [Authorize(Roles = "Customer,Staff,Manager")]
        public async Task<ActionResult<ChildComment>> UpdateAComment([FromBody] UpdatedCommentRequest updatedComment)
        {
            try
            {
                var user = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                var result = await _commentService.UpdateAComment(updatedComment.CommentId, updatedComment.Content, user.Id);
                if (result)
                {
                    return new JsonResult(new
                    {
                        status = "success"
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        status = "fail"
                    });
                }
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

        private int CountTotalComments(List<Tuple<ParentComment, List<ChildComment>>> list)
        {
            int count = 0;
            foreach (var item in list)
            {
                ++count;
                if (item.Item2.Count > 0)
                {
                    foreach (var childComment in item.Item2)
                    {
                        if (childComment != null)
                        {
                            ++count;
                        }
                    }
                }
            }
            return count;
        }
    }
}
