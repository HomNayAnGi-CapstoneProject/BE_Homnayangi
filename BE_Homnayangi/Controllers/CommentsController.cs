using BE_Homnayangi.Modules.CommentModule.Interface;
using BE_Homnayangi.Modules.CommentModule.Request;
using BE_Homnayangi.Modules.CommentModule.Response;
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

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/v1/comments/57448A79-8855-42AD-BD2E-0295D1436037
        [HttpGet("{blogId}")]
        public async Task<ActionResult<List<Tuple<ParentComment, List<ChildComment>>>>> GetCommentsByBlogId([FromRoute] Guid blogId)
        {
            var result = await _commentService.GetCommentsByBlogId(blogId);
            return new JsonResult(new
            {
                result = result
            });
        }

        [HttpPost]
        [Authorize(Roles = "Customer,Staff,Manager")]
        public async Task<ActionResult<bool>> CreateANewComment([FromBody] CreatedCommentRequest newComment)
        {
            var result = await _commentService.CreateANewComment(newComment);
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
        public async Task<ActionResult<ChildComment>> DeleteAComment([FromRoute] Guid commentId)
        {
            var result = await _commentService.DeleteAComment(commentId);
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

        [HttpPut]
        public async Task<ActionResult<ChildComment>> UpdateAComment([FromBody] UpdatedCommentRequest updatedComment)
        {
            var result = await _commentService.UpdateAComment(updatedComment.CommentId, updatedComment.Content);
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
    }
}
