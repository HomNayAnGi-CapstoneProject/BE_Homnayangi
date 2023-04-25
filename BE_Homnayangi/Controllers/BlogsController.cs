using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.Utils;
using Library.Models.Constant;
using Library.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [Authorize(Roles = "Manager,Staff")]
        public async Task<ActionResult> GetBlogsForStaff([FromQuery] bool? isEvent, [FromQuery] bool? isPending)
        {
            try
            {
                var currentAccount = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                var blogs = await _blogService.GetBlogsByUser(currentAccount.Role, isPending, isEvent);

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
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
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
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        // GET: api/v1/blogs/57448A79-8855-42AD-BD2E-0295D1436037

        [HttpGet("staff-preview/{id}")]
        [Authorize(Roles = "Manager,Staff")]
        public async Task<ActionResult<BlogDetailResponse>> GetBlogForPreview([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _blogService.GetBlogDetailPreview(id));
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

        [HttpGet("ingredients/{ingredientId}")]
        public async Task<ActionResult<ICollection<BlogsByCateAndTagResponse>>> GetBlogsByIngredientId([FromRoute] Guid ingredientId)
        {
            try
            {
                var result = await _blogService.GetBlogsByIngredientId(ingredientId);

                if (result.Count == 0)
                {
                    return new JsonResult(new
                    {
                        status = "failed",
                        msg = "Do not have any blog use this ingredient"
                    });
                }

                return new JsonResult(new
                {
                    status = "success",
                    result = result
                });
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
                var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                if (currentUser == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (currentUser.Role.Equals("Customer"))
                {
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_ALLOWED_TO_CREATE_BLOG);
                }

                // Role: User only
                var id = await _blogService.CreateEmptyBlog(currentUser.Id);
                return new JsonResult(new
                {
                    status = "success",
                    blog_id = id,
                });
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
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> DeleteBlog([FromRoute] Guid id)
        {
            try
            {
                #region Authorization
                var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                if (currentUser == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (currentUser.Role.Equals("Customer"))
                {
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_ALLOWED_TO_DELETE_BLOG);
                }
                else if (currentUser.Role.Equals("Admin"))
                {
                    throw new Exception(ErrorMessage.AdminError.ADMIN_NOT_ALLOWED_TO_DELETE_BLOG);
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
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
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
                var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                if (currentUser == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (currentUser.Role.Equals("Customer"))
                {
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_ALLOWED_TO_RESTORE_BLOG);
                }
                else if (currentUser.Role.Equals("Admin"))
                {
                    throw new Exception(ErrorMessage.AdminError.ADMIN_NOT_ALLOWED_TO_RESTORE_BLOG);
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
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        // REMOVE: api/Blogs/5
        [HttpDelete("remove-draft/{id}")]
        [Authorize(Roles = "Staff,Manager")]
        public async Task<IActionResult> RemoveBlogDraft(Guid id)
        {
            try
            {
                #region Authorization
                var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                if (currentUser == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (currentUser.Role.Equals("Customer"))
                {
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_ALLOWED_TO_DELETE_BLOG);
                }
                else if (currentUser.Role.Equals("Admin"))
                {
                    throw new Exception(ErrorMessage.AdminError.ADMIN_NOT_ALLOWED_TO_DELETE_BLOG);
                }
                #endregion

                await _blogService.RemoveBlogDraft(id);
                return Ok();
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
        #endregion

        [HttpGet("category/sub-categories")]
        public async Task<ActionResult<PagedResponse<PagedList<BlogsByCateAndTagResponse>>>> GetBlogsBySubCates([FromQuery] BlogsBySubCatesRequest request)
        {
            var response = await _blogService.GetBlogsBySubCates(request);

            if (response.Resource.Count == 0)
            {
                return new JsonResult(new
                {
                    status = "failed"
                });
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet("suggest-blog/{Age}/{IsMale}/{IsLoseWeight}")]
        public async Task<ActionResult<SuggestBlogResponse>> GetSuggestBlogs([FromRoute] SuggestBlogByCaloRequest request)
        {
            try
            {
                return Ok(await _blogService.GetSuggestBlogByCalo(request));
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

        /*        [Authorize(Roles = "Manager")]*/
        [HttpPut("{type}/{blogId}")]
        public async Task<ActionResult> ApproveRejectBlog([FromRoute] string type, [FromRoute] Guid blogId)
        {
            try
            {
                if (!(type.Equals("APPROVE") || type.Equals("REJECT")))
                {
                    throw new Exception(ErrorMessage.BlogError.BLOG_MNG_NOT_SUPPORT);
                }

                bool isChecked = await _blogService.ApproveRejectBlog(type, blogId);
                if (isChecked)
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
                        status = "failed",
                        msg = "Some of fields can not be null or empty"
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


    }
}
