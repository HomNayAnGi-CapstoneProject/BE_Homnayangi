using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.DataAccess;
using Library.Models;
using Library.Models.DTO.CategoryDTO;
using BE_Homnayangi.Modules.CategoryModule.Interface;

namespace BE_Homnayangi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly HomnayangiContext _context;
        private ICategoryService categoryService;

        public CategoriesController(HomnayangiContext context, IMapper mapper, ICategoryService _categoryService)
        {
            _context = context;
            _mapper = mapper;
            categoryService = _categoryService;
        }


        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var category = await categoryService.GetAll();
            return Ok(category);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory(Guid id)
        {
            var category = await categoryService.GetCategoriesBy(x => x.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CreateCategoryRequest reqCategory)
        {
            var box = _mapper.Map<Category>(reqCategory);

            var checkValid = new CreateCategoryRequestValidator().Validate(reqCategory);
            if (!checkValid.IsValid) return NotFound();

            _context.Categories.Add(box);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(box.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategory", new { id = box.CategoryId }, box);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
