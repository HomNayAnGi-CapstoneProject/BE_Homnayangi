using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Commons;
using Library.DataAccess;
using Library.Models.Constant;
using Library.PagedList;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly HomnayangiContext _context;
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;

        public BlogsController(IBlogService blogService, IUserService userService)
        {
            _blogService = blogService;
            _userService = userService;
        }
        // Get all blogs: staff and manager manage all blogs of system
        [HttpGet("user")] // blogid, authorName, img, title, created_date, views, reactions, status
        public async Task<ActionResult> GetBlogsByUser()
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (_userService.GetCurrentUser(Request.Headers["Authorization"]).Role.Equals(CommonEnum.RoleEnum.CUSTOMER))
                {
                    throw new Exception(ErrorMessage.UserError.ACTION_FOR_STAFF_AND_MANAGER_ROLE);
                }

                var blogs = await _blogService.GetBlogsByUser();
                if (blogs == null || blogs.Count == 0)
                {
                    return new JsonResult(new
                    {
                        total_result = 0
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        total_result = blogs.Count,
                        result = blogs
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/v1/blogs/57448A79-8855-42AD-BD2E-0295D1436037
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDetailResponse>> GetBlogDetail([FromRoute] Guid id)
        {
            try
            {
                return Ok(_blogService.GetBlogDetail(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/v1/blogs/57448A79-8855-42AD-BD2E-0295D1436037
        [HttpGet("staff-preview/{id}")]
        public async Task<ActionResult<BlogDetailResponse>> GetBlogForPreview([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _blogService.GetBlogDetailPreview(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region CUD Blog
        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> CreateEmptyBlog()
        {
            try
            {

                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (_userService.GetCurrentUser(Request.Headers["Authorization"]).Role.Equals("Customer"))
                {
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_ALLOWED_TO_CREATE_BLOG);
                }

                // Role: User only
                var id = await _blogService.CreateEmptyBlog(_userService.GetCurrentUser(Request.Headers["Authorization"]).Id);
                return new JsonResult(new
                {
                    status = "success",
                    blog_id = id,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutBlog([FromBody] BlogUpdateRequest request)
        {
            try
            {
                var currentUserId = _userService.GetCurrentUser(Request.Headers["Authorization"]).Id;
                await _blogService.UpdateBlog(request, currentUserId); ;
                return Ok("Update success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // REMOVE: api/Blogs/5
        [HttpDelete("remove-draft/{id}")]
        public async Task<IActionResult> RemoveBlogDraft(Guid id)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (_userService.GetCurrentUser(Request.Headers["Authorization"]).Role.Equals("Customer"))
                {
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_ALLOWED_TO_CREATE_BLOG);
                }

                await _blogService.RemoveBlogDraft(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        [HttpGet("category/tag")]
        public async Task<ActionResult<PagedResponse<PagedList<BlogsByCateAndTagResponse>>>> GetBlogsByCateAndTag([FromQuery] BlogFilterByCateAndTagRequest blogFilter)
        {
            var response = await _blogService.GetBlogsByCategoryAndTag(blogFilter);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("category/sub-categories")]
        public async Task<ActionResult<PagedResponse<PagedList<BlogsByCateAndTagResponse>>>> GetBlogsBySubCates([FromQuery] BlogsBySubCatesRequest request)
        {
            var response = await _blogService.GetBlogsBySubCates(request);

            if (response.Resource.Count == 0)
            {
                return NotFound();
            }

            return Ok(response);
        }

    }
}
