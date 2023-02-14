using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Response;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.DataAccess;
using Library.Models;
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

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(Guid id, Blog blog)
        {
            if (id != blog.BlogId)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> CreateEmptyBlog()
        {
            Guid authorId = _userService.GetCurrentLoginUserId(Request.Headers["Authorization"]);
            var id = await _blogService.CreateEmptyBlog(authorId);
            if (id.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                return new JsonResult(new
                {
                    status = "fail"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    status = "success",
                    blog_id = id,
                });
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

        private bool BlogExists(Guid id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
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

        [HttpPatch]
        public async Task<IActionResult> PatchBlog([FromBody] BlogUpdateRequest request)
        {
            await _blogService.UpdateBlog(request);

            return NoContent();
        }
    }
}
