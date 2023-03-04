using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Commons;
using BE_Homnayangi.Modules.Utils;
using Library.DataAccess;
using Library.Models.Constant;
using Library.PagedList;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.Collections.Generic;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;

        public BlogsController(IBlogService blogService, ISubCateService subCateService, 
            IUserService userService, ICustomAuthorization customAuthorization)
        {
            _blogService = blogService;
            _userService = userService;
        }

        #region Get
        // Get all blogs: staff and manager manage all blogs of system
        [HttpGet("user")] // blogid, authorName, img, title, created_date, views, reactions, status
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult> GetBlogsForStaff()
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
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
                return Ok(await _blogService.GetBlogDetail(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/v1/blogs/57448A79-8855-42AD-BD2E-0295D1436037

        [HttpGet("staff-preview/{id}")]
        [Authorize(Roles = "Staff")]
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
        #endregion

        #region CUD Blog
        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Staff,Manager")]
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
        [Authorize(Roles = "Staff,Manager")]
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
        [Authorize(Roles = "Staff.Manager")]
        public async Task<IActionResult> DeleteBlog([FromRoute] Guid id)
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
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_ALLOWED_TO_DELETE_BLOG);
                }
                #endregion

                await _blogService.DeleteBlog(id);
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

        // RESTORE: api/Blogs/5
        [HttpPut("restore-blog/{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> RestoreBlog([FromRoute] Guid id)
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
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_ALLOWED_TO_RESTORE_BLOG);
                }
                #endregion

                await _blogService.RestoreBlog(id);
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

        // REMOVE: api/Blogs/5
        [HttpDelete("remove-draft/{id}")]
        [Authorize(Roles = "Staff,Manager")]
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

        [HttpGet("suggest-blog/{Age}/{IsMale}/{IsLoseWeight}")]
        public async Task<ActionResult<ICollection<OverviewBlogResponse>>> GetSuggestBlogs([FromRoute] SuggestBlogByCaloRequest request)
        {
            try
            {
                return Ok(await _blogService.GetSuggestBlogByCalo(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
