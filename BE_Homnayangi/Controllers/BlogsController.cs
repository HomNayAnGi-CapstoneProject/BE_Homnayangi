using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.TagModule.Interface;
using BE_Homnayangi.Modules.TagModule.Response;
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
        private readonly ITagService _tagService;

        public BlogsController(HomnayangiContext context, IBlogService blogService, ITagService tagService)
        {
            _context = context;
            _blogService = blogService;
            _tagService = tagService;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            return await _context.Blogs.ToListAsync();
        }

        [HttpGet("/api/v1/blogs/test")] // sort ascending by cookedprice
        public async Task<ActionResult<IEnumerable<GetBlogsForHomePageResponse>>> GetTest(Guid categoryId, [FromQuery(Name = "numberItems")] int numberItems = 4)
        {
            var blogs = await _blogService.GetBlogsByCategory(categoryId, numberItems);

            return new JsonResult(new
            {
                total_results = blogs.Count(),
                result = blogs,
            }); ;
        }


        // GET: api/blogs/FA1CDF34-652D-4E8E-8BAF-19917D31772A
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDetailResponse>> GetBlog(Guid id) // tạm thời chưa lấy author_name
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


        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<BlogResponse>>> GetBlogsByCategory(Guid categoryId, [FromQuery(Name = "numberItems")] int numberItems = 4)
        {
            var blogs = await _blogService.GetBlogsByCategory(categoryId, numberItems);

            return new JsonResult(new
            {
                total_results = blogs.Count(),
                result = blogs,
            }); ;
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
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BlogExists(blog.BlogId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
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
        // Get tags by categoryId 
        [HttpGet("tags/{categoryId}")]
        public async Task<ActionResult<IEnumerable<TagResponse>>> GetTagsByCategoryId(Guid categoryId)
        {
            var tags = await _tagService.GetTagsByCategoryId(categoryId);

            return new JsonResult(new
            {
                total_results = tags.Count(),
                result = tags,
            }); ;
        }
    }
}
