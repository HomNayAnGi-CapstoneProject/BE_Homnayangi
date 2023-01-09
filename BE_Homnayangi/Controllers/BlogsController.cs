using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.DataAccess;
using Library.Models;
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
        private readonly IRecipeService _recipeService;

        public BlogsController(HomnayangiContext context, IBlogService blogService, IRecipeService recipeService)
        {
            _context = context;
            _blogService = blogService;
            _recipeService = recipeService;
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


        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(Guid id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
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
        public async Task<ActionResult<Blog>> GetBlogsByCateAndTag([FromQuery] BlogFilterByCateAndTagRequest blogFilter)
        {
            var response = await _blogService.GetBlogsByCategoryAndTag(blogFilter);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
