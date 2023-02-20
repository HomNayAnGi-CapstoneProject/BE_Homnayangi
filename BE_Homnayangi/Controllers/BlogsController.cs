using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Response;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.UserModule.Response;
using Library.DataAccess;
using Library.Models;
using Library.Models.Constant;
using Library.PagedList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly HomnayangiContext _context;
        private readonly IBlogService _blogService;
        private readonly ISubCateService _subCateService;
        private readonly IUserService _userService;

        public BlogsController(HomnayangiContext context, IBlogService blogService, ISubCateService subCateService, IUserService userService)
        {
            _context = context;
            _blogService = blogService;
            _subCateService = subCateService;
            _userService = userService;
        }

        // Get all blogs: staff and manager manage all blogs of system
        [HttpGet("user")] // blogid, authorName, img, title, created_date, views, reactions, status
        public async Task<ActionResult> GetBlogsByUser([FromRoute] Guid userId)
        {
            try
            {
                CurrentUserResponse currentUser = _userService.GetCurrentLoginUser();
                if (currentUser == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (currentUser.Role.Equals("Customer"))
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

        // Get all blogs(customer)
        [HttpGet("customer")] // Available blogs: blogid, recipeName, title, description, img, created_date, views, reactions,
                              // cooked_price, subcates[] -> tags,
                              // Recipe (Kcal, cookedPrice)
        public async Task<ActionResult> GetBlogsByCustomer()
        {
            try
            {
                CurrentUserResponse currentUser = _userService.GetCurrentLoginUser();
                if (currentUser == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (!currentUser.Role.Equals("Customer"))
                {
                    throw new Exception(ErrorMessage.UserError.ACTION_FOR_USER_ROLE_ONLY);
                }

                var blogs = await _blogService.GetBlogsByCustomer();
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


        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            return await _context.Blogs.ToListAsync();
        }


        // GET: api/v1/blogs/57448A79-8855-42AD-BD2E-0295D1436037
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDetailResponse>> GetBlog([FromRoute] Guid id)
        {
            var blog = await _blogService.GetBlogDetails(id);

            if (blog == null)
            {
                return NotFound();
            }

            return new JsonResult(new
            {
                result = blog
            });
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

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> CreateEmptyBlog()
        {
            try
            {
                CurrentUserResponse currentUser = _userService.GetCurrentLoginUser();
                if (currentUser == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }
                else if (!currentUser.Role.Equals("User"))
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
                await _blogService.RemoveBlogDraft(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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

        // Get sub-cates by categoryId 
        [HttpGet("categories/{categoryId}/sub-categories")]
        public async Task<ActionResult<IEnumerable<SubCateResponse>>> GetSubCatesByCategoryId(Guid categoryId)
        {
            var subCates = await _subCateService.GetSubCatesByCategoryId(categoryId);

            return new JsonResult(new
            {
                total_results = subCates.Count(),
                result = subCates,
            }); ;
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

        [HttpPut]
        public async Task<IActionResult> PutBlog([FromBody] BlogUpdateRequest request)
        {
            try
            {
                var currentUserId = _userService.GetCurrentLoginUser().Id;
                await _blogService.UpdateBlog(request, currentUserId); ;
                return Ok("Update success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
